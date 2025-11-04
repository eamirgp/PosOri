using MediatR;
using Pos.Application.Contracts.Persistence;
using Pos.Application.Shared.Result;
using Pos.Domain.Inputs;

namespace Pos.Application.Features.InventoryAdjustment.Commands.CreateInventoryAdjustment
{
    public class CreateInventoryAdjustmentRequestHandler : IRequestHandler<CreateInventoryAdjustmentRequest, Result<Guid>>
    {
        private readonly IInventoryAdjustmentRepository _inventoryAdjustmentRepository;
        private readonly IAdjustmentTypeRepository _adjustmentTypeRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IProductRepository _productRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateInventoryAdjustmentRequestHandler(
            IInventoryAdjustmentRepository inventoryAdjustmentRepository,
            IAdjustmentTypeRepository adjustmentTypeRepository,
            IWarehouseRepository warehouseRepository,
            IProductRepository productRepository,
            IInventoryRepository inventoryRepository,
            IUnitOfWork unitOfWork
            )
        {
            _inventoryAdjustmentRepository = inventoryAdjustmentRepository;
            _adjustmentTypeRepository = adjustmentTypeRepository;
            _warehouseRepository = warehouseRepository;
            _productRepository = productRepository;
            _inventoryRepository = inventoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateInventoryAdjustmentRequest request, CancellationToken cancellationToken)
        {
            // Validate warehouse exists
            var warehouse = await _warehouseRepository.GetByIdAsync(request.WarehouseId);
            if (warehouse is null)
                return Result<Guid>.Failure(new List<string> { $"El almacén con ID '{request.WarehouseId}' no existe." }, 404);

            // Validate adjustment type exists
            var adjustmentType = await _adjustmentTypeRepository.GetByIdAsync(request.AdjustmentTypeId);
            if (adjustmentType is null)
                return Result<Guid>.Failure(new List<string> { $"El tipo de ajuste con ID '{request.AdjustmentTypeId}' no existe." }, 404);

            // Validate products exist
            var productIds = request.Details.Select(d => d.ProductId).ToList();
            var products = await _productRepository.GetByIdsAsync(productIds);
            var productsDictionary = products.ToDictionary(p => p.Id);

            List<string> errors = new();

            foreach (var detail in request.Details)
            {
                if (!productsDictionary.ContainsKey(detail.ProductId))
                    errors.Add($"El producto con ID '{detail.ProductId}' no existe.");
            }

            if (errors.Any())
                return Result<Guid>.Failure(errors, 404);

            // Create adjustment input
            var detailsInput = request.Details.Select(d => new InventoryAdjustmentDetailInput(
                productsDictionary[d.ProductId],
                d.Quantity,
                d.Notes
                )).ToList();

            // Create inventory adjustment entity
            Domain.Entities.InventoryAdjustment adjustment;

            try
            {
                adjustment = Domain.Entities.InventoryAdjustment.Create(
                    request.WarehouseId,
                    request.AdjustmentTypeId,
                    request.Date,
                    request.Reason,
                    detailsInput
                    );
            }
            catch (ArgumentException ex)
            {
                return Result<Guid>.Failure(new List<string> { ex.Message }, 400);
            }

            await _inventoryAdjustmentRepository.CreateAsync(adjustment);

            // Get existing inventories
            var inventories = await _inventoryRepository.GetByProductsAndWarehouse(productIds, warehouse.Id);
            var inventoriesDictionary = inventories.ToDictionary(i => i.ProductId);

            var newInventories = new List<Domain.Entities.Inventory>();

            // Apply stock adjustments based on adjustment type
            bool isNegativeAdjustment = adjustmentType.Code == "NEGATIVE";

            foreach (var detail in request.Details)
            {
                if (inventoriesDictionary.TryGetValue(detail.ProductId, out var inventory))
                {
                    // Update existing inventory
                    if (isNegativeAdjustment)
                    {
                        try
                        {
                            inventory.DecreaseStock(detail.Quantity);
                        }
                        catch (InvalidOperationException ex)
                        {
                            var product = productsDictionary[detail.ProductId];
                            return Result<Guid>.Failure(
                                new List<string> { $"Stock insuficiente para el producto '{product.Name.Value}'. {ex.Message}" },
                                400
                            );
                        }
                    }
                    else // Initial or Positive
                    {
                        inventory.IncreaseStock(detail.Quantity);
                    }
                }
                else
                {
                    // Create new inventory only for Initial or Positive adjustments
                    if (isNegativeAdjustment)
                    {
                        var product = productsDictionary[detail.ProductId];
                        return Result<Guid>.Failure(
                            new List<string> { $"No existe inventario para el producto '{product.Name.Value}' en este almacén. No se puede realizar un ajuste negativo." },
                            400
                        );
                    }

                    var newInventory = Domain.Entities.Inventory.Create(
                        detail.ProductId,
                        warehouse.Id,
                        detail.Quantity
                        );

                    newInventories.Add(newInventory);
                }
            }

            if (newInventories.Any())
                await _inventoryRepository.CreateRangeAsync(newInventories);

            await _unitOfWork.SaveChangesAsync();

            return Result<Guid>.Success(adjustment.Id);
        }
    }
}
