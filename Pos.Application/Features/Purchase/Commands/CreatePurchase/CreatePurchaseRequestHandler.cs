using MediatR;
using Pos.Application.Contracts.Persistence;
using Pos.Application.Shared.Result;
using Pos.Domain.Inputs;

namespace Pos.Application.Features.Purchase.Commands.CreatePurchase
{
    public class CreatePurchaseRequestHandler : IRequestHandler<CreatePurchaseRequest, Result<Guid>>
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IVoucherTypeRepository _voucherTypeRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IProductRepository _productRepository;
        private readonly IIGVTypeRepository _igvTypeRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IInventoryMovementRepository _inventoryMovementRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePurchaseRequestHandler(
            IPurchaseRepository purchaseRepository,
            IWarehouseRepository warehouseRepository,
            IVoucherTypeRepository voucherTypeRepository,
            ICurrencyRepository currencyRepository,
            IPersonRepository personRepository,
            IProductRepository productRepository,
            IIGVTypeRepository iGVTypeRepository,
            IInventoryRepository inventoryRepository,
            IInventoryMovementRepository inventoryMovementRepository,
            IUnitOfWork unitOfWork
            )
        {
            _purchaseRepository = purchaseRepository;
            _warehouseRepository = warehouseRepository;
            _voucherTypeRepository = voucherTypeRepository;
            _currencyRepository = currencyRepository;
            _personRepository = personRepository;
            _productRepository = productRepository;
            _igvTypeRepository = iGVTypeRepository;
            _inventoryRepository = inventoryRepository;
            _inventoryMovementRepository = inventoryMovementRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreatePurchaseRequest request, CancellationToken cancellationToken)
        {
            var warehouse = await _warehouseRepository.GetByIdAsync(request.WarehouseId);
            if (warehouse is null)
                return Result<Guid>.Failure(new List<string> { $"El almacén con ID '{request.WarehouseId}' no existe." }, 404);

            var voucherType = await _voucherTypeRepository.GetByIdAsync(request.VoucherTypeId);
            if (voucherType is null)
                return Result<Guid>.Failure(new List<string> { $"El tipo de comprobante con ID '{request.VoucherTypeId}' no existe." }, 404);

            var currency = await _currencyRepository.GetByIdAsync(request.CurrencyId);
            if (currency is null)
                return Result<Guid>.Failure(new List<string> { $"La moneda con ID '{request.CurrencyId}' no existe." }, 404);

            var person = await _personRepository.GetByIdAsync(request.PersonId);
            if (person is null)
                return Result<Guid>.Failure(new List<string> { $"El proveedor con ID '{request.PersonId}' no existe." }, 404);

            if (await _purchaseRepository.ExistVoucherNumber(request.Serie, request.Number, voucherType.Id, person.Id))
                return Result<Guid>.Failure(new List<string> { $"El documento '{request.Serie}-{request.Number}' ya existe para este proveedor." }, 409);

            var productIds = request.Details.Select(d => d.ProductId).ToList();
            var products = await _productRepository.GetByIdsAsync(productIds);
            var productsDictionary = products.ToDictionary(p => p.Id);

            var igvTypeIds = request.Details.Select(d => d.IGVTypeId).Distinct().ToList();
            var igvTypes = await _igvTypeRepository.GetByIdsAsync(igvTypeIds);
            var igvTypesDictionary = igvTypes.ToDictionary(it => it.Id);

            List<string> errors = new();

            foreach (var detail in request.Details)
            {
                if (!productsDictionary.ContainsKey(detail.ProductId))
                    errors.Add($"El producto con ID '{detail.ProductId}' no existe.");

                if (!igvTypesDictionary.ContainsKey(detail.IGVTypeId))
                    errors.Add($"El tipo de IGV con ID '{detail.IGVTypeId}' no existe.");
            }

            if (errors.Any())
                return Result<Guid>.Failure(errors, 404);

            var detailslInput = request.Details.Select(d => new PurchaseDetailInput(
                productsDictionary[d.ProductId],
                igvTypesDictionary[d.IGVTypeId],
                d.Quantity,
                d.UnitValue
                )).ToList();

            var purchase = Domain.Entities.Purchase.Create(
                request.WarehouseId,
                request.VoucherTypeId,
                request.CurrencyId,
                request.PersonId,
                request.Serie,
                request.Number,
                request.IssueDate,
                detailslInput
                );

            await _purchaseRepository.CreateAsync(purchase);

            var inventories = await _inventoryRepository.GetByProductsAndWarehouse(productIds, warehouse.Id);
            var inventoriesDictionary = inventories.ToDictionary(i => i.ProductId);

            var newInventories = new List<Domain.Entities.Inventory>();

            var movements = new List<Domain.Entities.InventoryMovement>();

            foreach (var detail in request.Details)
            {
                var previousStock = inventoriesDictionary.ContainsKey(detail.ProductId)
                    ? inventoriesDictionary[detail.ProductId].Stock.Value
                    : 0;

                if (inventoriesDictionary.TryGetValue(detail.ProductId, out var inventory))
                {
                    inventory.IncreaseStock(detail.Quantity);
                }
                else
                {
                    var newInventory = Domain.Entities.Inventory.Create(
                        detail.ProductId,
                        warehouse.Id,
                        detail.Quantity
                        );

                    newInventories.Add(newInventory);
                }

                var movement = Domain.Entities.InventoryMovement.CreatePurchaseMovement(
                    detail.ProductId,
                    warehouse.Id,
                    detail.Quantity,
                    purchase.Id,
                    previousStock
                    );

                movements.Add(movement);
            }

            if (newInventories.Any())
                await _inventoryRepository.CreateRangeAsync(newInventories);

            await _inventoryMovementRepository.CreateRangeAsync(movements);

            await _unitOfWork.SaveChangesAsync();

            return Result<Guid>.Success(purchase.Id);
        }
    }
}
