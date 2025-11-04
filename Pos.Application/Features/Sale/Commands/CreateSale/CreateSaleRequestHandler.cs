using MediatR;
using Pos.Application.Contracts.Persistence;
using Pos.Application.Shared.Result;
using Pos.Domain.Inputs;

namespace Pos.Application.Features.Sale.Commands.CreateSale
{
    public class CreateSaleRequestHandler : IRequestHandler<CreateSaleRequest, Result<Guid>>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IVoucherTypeRepository _voucherTypeRepository;
        private readonly IVoucherSerieRepository _voucherSerieRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IProductRepository _productRepository;
        private readonly IIGVTypeRepository _igvTypeRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSaleRequestHandler(
            ISaleRepository saleRepository,
            IWarehouseRepository warehouseRepository,
            IVoucherTypeRepository voucherTypeRepository,
            IVoucherSerieRepository voucherSerieRepository,
            ICurrencyRepository currencyRepository,
            IPersonRepository personRepository,
            IProductRepository productRepository,
            IIGVTypeRepository igvTypeRepository,
            IInventoryRepository inventoryRepository,
            IUnitOfWork unitOfWork
            )
        {
            _saleRepository = saleRepository;
            _warehouseRepository = warehouseRepository;
            _voucherTypeRepository = voucherTypeRepository;
            _voucherSerieRepository = voucherSerieRepository;
            _currencyRepository = currencyRepository;
            _personRepository = personRepository;
            _productRepository = productRepository;
            _igvTypeRepository = igvTypeRepository;
            _inventoryRepository = inventoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateSaleRequest request, CancellationToken cancellationToken)
        {
            var warehouse = await _warehouseRepository.GetByIdAsync(request.WarehouseId);
            if (warehouse is null)
                return Result<Guid>.Failure(new List<string> { $"El almacén con ID '{request.WarehouseId}' no existe." }, 404);

            var voucherType = await _voucherTypeRepository.GetByIdAsync(request.VoucherTypeId);
            if (voucherType is null)
                return Result<Guid>.Failure(new List<string> { $"El tipo de comprobante con ID '{request.VoucherTypeId}' no existe." }, 404);

            var voucherSerie = await _voucherSerieRepository.GetByVoucherTypeAsync(voucherType.Id);
                if (voucherSerie is null)
                return Result<Guid>.Failure(new List<string> { $"No existe una serie para el tipo de comprobante '{voucherType.Description}'." }, 404);
            
            var currency = await _currencyRepository.GetByIdAsync(request.CurrencyId);
            if (currency is null)
                return Result<Guid>.Failure(new List<string> { $"La moneda con ID '{request.CurrencyId}' no existe." }, 404);

            var person = await _personRepository.GetByIdAsync(request.PersonId);
            if (person is null)
                return Result<Guid>.Failure(new List<string> { $"El cliente con ID '{request.PersonId}' no existe." }, 404);

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

            var inventories = await _inventoryRepository.GetByProductsAndWarehouse(productIds, warehouse.Id);
            var inventoriesDictionary = inventories.ToDictionary(i => i.ProductId);

            List<string> stockErrors = new();

            foreach (var detail in request.Details)
            {
                if (!inventoriesDictionary.ContainsKey(detail.ProductId))
                {
                    var productName = productsDictionary[detail.ProductId].Name.Value;
                    stockErrors.Add($"El producto '{productName}' no cuenta con inventario.");
                }
                else if (inventoriesDictionary[detail.ProductId].Stock.Value < detail.Quantity)
                {
                    var productName = productsDictionary[detail.ProductId].Name.Value;
                    var available = inventoriesDictionary[detail.ProductId].Stock.Value;
                    stockErrors.Add($"Stock insuficiente para '{productName}'. Disponible: {available}, Solicitado: {detail.Quantity}");
                }
            }

            if (stockErrors.Any())
                return Result<Guid>.Failure(stockErrors, 400);

            var serie = voucherSerie.Serie;
            var number = voucherSerie.GetNextNumber();

            var detailsInput = request.Details.Select(d => new SaleDetailInput(
                productsDictionary[d.ProductId],
                igvTypesDictionary[d.IGVTypeId],
                d.Quantity,
                d.UnitPrice
                )).ToList();

            var sale = Domain.Entities.Sale.Create(
                warehouse.Id,
                voucherType.Id,
                currency.Id,
                person.Id,
                serie,
                number,
                DateTime.Now,
                detailsInput
                );

            await _saleRepository.CreateAsync(sale);

            foreach (var detail in request.Details)
            {
                inventoriesDictionary[detail.ProductId].DecreaseStock(detail.Quantity);
            }

            await _unitOfWork.SaveChangesAsync();

            return Result<Guid>.Success(sale.Id);
        }
    }
}
