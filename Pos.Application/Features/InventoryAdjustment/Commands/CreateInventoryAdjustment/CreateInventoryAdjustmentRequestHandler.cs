using MediatR;
using Pos.Application.Contracts.Persistence;
using Pos.Application.Shared.Result;
using Pos.Domain.Entities;
using Pos.Domain.Inputs;

namespace Pos.Application.Features.InventoryAdjustment.Commands.CreateInventoryAdjustment
{
    public class CreateInventoryAdjustmentRequestHandler : IRequestHandler<CreateInventoryAdjustmentRequest, Result<Guid>>
    {
        private readonly IInventoryAdjustmentRepository _inventoryAdjustmentRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IInventoryAdjustmentTypeRepository _inventoryAdjustmentTypeRepository;
        private readonly IProductRepository _productRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IInventoryMovementRepository _inventoryMovementRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateInventoryAdjustmentRequestHandler(
            IInventoryAdjustmentRepository inventoryAdjustmentRepository,
            IWarehouseRepository warehouseRepository,
            IInventoryAdjustmentTypeRepository inventoryAdjustmentTypeRepository,
            IProductRepository productRepository,
            IInventoryRepository inventoryRepository,
            IInventoryMovementRepository inventoryMovementRepository,
            IUnitOfWork unitOfWork
            )
        {
            _warehouseRepository = warehouseRepository;
            _inventoryAdjustmentRepository = inventoryAdjustmentRepository;
            _inventoryAdjustmentTypeRepository = inventoryAdjustmentTypeRepository;
            _productRepository = productRepository;
            _inventoryRepository = inventoryRepository;
            _inventoryMovementRepository = inventoryMovementRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateInventoryAdjustmentRequest request, CancellationToken cancellationToken)
        {
            var warehouse = await _warehouseRepository.GetByIdAsync(request.WarehouseId);
            if (warehouse is null)
                return Result<Guid>.Failure(new List<string> { $"El almacén con ID '{request.WarehouseId}' no existe." }, 404);

            var inventoryAdjustmentType = await _inventoryAdjustmentTypeRepository.GetByIdAsync(request.InventoryAdjustmentTypeId);
            if (inventoryAdjustmentType is null)
                return Result<Guid>.Failure(new List<string> { $"El tipo de ajuste de inventario con ID '{request.InventoryAdjustmentTypeId}' no existe." }, 404);

            var productIds = request.Details.Select(d => d.ProductId).ToList();
            var products = await _productRepository.GetByIdsAsync(productIds);
            var productsDictionary = products.ToDictionary(p => p.Id);

            var errors = new List<string>();

            foreach (var detail in request.Details)
            {
                if (!productsDictionary.ContainsKey(detail.ProductId))
                    errors.Add($"El producto con ID '{detail.ProductId}' no existe.");
            }

            if (errors.Any())
                return Result<Guid>.Failure(errors, 404);

            var inventories = await _inventoryRepository.GetByProductsAndWarehouse(productIds, warehouse.Id);
            var inventoriesDictionary = inventories.ToDictionary(i => i.ProductId);

            if (inventoryAdjustmentType.Code == "DEC")
            {
                var stockErrors = new List<string>();

                foreach (var detail in request.Details)
                {
                    var productName = productsDictionary[detail.ProductId].Name.Value;

                    if (!inventoriesDictionary.ContainsKey(detail.ProductId))
                    {
                        stockErrors.Add($"El producto '{productName}' no cuenta con inventario en este almacén.");
                    }
                    else if (inventoriesDictionary[detail.ProductId].Stock.Value < detail.Quantity)
                    {
                        var available = inventoriesDictionary[detail.ProductId].Stock.Value;
                        stockErrors.Add($"Stock insuficiente para '{productName}'. Disponible: {available}, Solicitado: {detail.Quantity}");
                    }
                }

                if (stockErrors.Any())
                    return Result<Guid>.Failure(stockErrors, 400);
            }

            var detailsInput = request.Details.Select(d => new InventoryAdjustmentDetailInput(
                productsDictionary[d.ProductId],
                d.Quantity
                )).ToList();

            var inventoryAdjustment = Domain.Entities.InventoryAdjustment.Create(
                warehouse.Id,
                inventoryAdjustmentType.Id,
                request.Reason,
                detailsInput
                );

            await _inventoryAdjustmentRepository.CreateAsync(inventoryAdjustment);

            var newInventories = new List<Domain.Entities.Inventory>();
            bool isIncrease = inventoryAdjustmentType.Code == "INC";

            var movements = new List<InventoryMovement>();

            foreach (var detail in request.Details)
            {
                var previousStock = inventoriesDictionary.ContainsKey(detail.ProductId)
                    ? inventoriesDictionary[detail.ProductId].Stock.Value
                    : 0;

                if (inventoriesDictionary.TryGetValue(detail.ProductId, out var inventory))
                {
                    if (isIncrease)
                        inventory.IncreaseStock(detail.Quantity);
                    else
                        inventory.DecreaseStock(detail.Quantity);
                }
                else
                {
                    if (isIncrease)
                    {
                        var newInventory = Domain.Entities.Inventory.Create(
                            detail.ProductId,
                            warehouse.Id,
                            detail.Quantity
                            );

                        newInventories.Add(newInventory);
                    }
                }

                var movement = InventoryMovement.CreateAdjustmentMovement(
                    detail.ProductId,
                    warehouse.Id,
                    detail.Quantity,
                    inventoryAdjustment.Id,
                    previousStock,
                    isIncrease
                    );

                movements.Add(movement);
            }

            if (newInventories.Any())
                await _inventoryRepository.CreateRangeAsync(newInventories);

            await _inventoryMovementRepository.CreateRangeAsync(movements);

            await _unitOfWork.SaveChangesAsync();

            return Result<Guid>.Success(inventoryAdjustment.Id);
        }
    }
}
