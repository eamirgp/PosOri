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
        private readonly IUnitOfMeasureRepository _unitOfMeasureRepository;
        private readonly IIGVTypeRepository _igvTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSaleRequestHandler(
            ISaleRepository saleRepository,
            IWarehouseRepository warehouseRepository,
            IVoucherTypeRepository voucherTypeRepository,
            IVoucherSerieRepository voucherSerieRepository,
            ICurrencyRepository currencyRepository,
            IPersonRepository personRepository,
            IProductRepository productRepository,
            IUnitOfMeasureRepository unitOfMeasureRepository,
            IIGVTypeRepository igvTypeRepository,
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
            _unitOfMeasureRepository = unitOfMeasureRepository;
            _igvTypeRepository = igvTypeRepository;
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

            var serie = voucherSerie.Serie;
            var number = voucherSerie.GetNextNumber();

            var currency = await _currencyRepository.GetByIdAsync(request.CurrencyId);
            if (currency is null)
                return Result<Guid>.Failure(new List<string> { $"La moneda con ID '{request.CurrencyId}' no existe." }, 404);

            var person = await _personRepository.GetByIdAsync(request.PersonId);
            if (person is null)
                return Result<Guid>.Failure(new List<string> { $"El cliente con ID '{request.PersonId}' no existe." }, 404);

            var productIds = request.Details.Select(d => d.ProductId).ToList();
            var products = await _productRepository.GetByIdsAsync(productIds);
            var productsDictionary = products.ToDictionary(p => p.Id);

            var unitOfMeasureIds = request.Details.Select(d => d.UnitOfMeasureId).Distinct().ToList();
            var unitOfMeasures = await _unitOfMeasureRepository.GetByIdsAsync(unitOfMeasureIds);
            var unitOfMeasuresDictionary = unitOfMeasures.ToDictionary(um => um.Id);

            var igvTypeIds = request.Details.Select(d => d.IGVTypeId).Distinct().ToList();
            var igvTypes = await _igvTypeRepository.GetByIdsAsync(igvTypeIds);
            var igvTypesDcitionary = igvTypes.ToDictionary(it => it.Id);

            List<string> errors = new();

            foreach (var detail in request.Details)
            {
                if (!productsDictionary.ContainsKey(detail.ProductId))
                    errors.Add($"El producto con ID '{detail.ProductId}' no existe.");

                if (!unitOfMeasuresDictionary.ContainsKey(detail.UnitOfMeasureId))
                    errors.Add($"La unidad de medida con ID '{detail.UnitOfMeasureId}' no existe.");

                if (!igvTypesDcitionary.ContainsKey(detail.IGVTypeId))
                    errors.Add($"El tipo de IGV con ID '{detail.IGVTypeId}' no existe.");
            }

            if (errors.Any())
                return Result<Guid>.Failure(errors, 404);

            var detailsInput = request.Details.Select(d => new SaleDetailInput(
                d.ProductId,
                d.UnitOfMeasureId,
                igvTypesDcitionary[d.IGVTypeId],
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
                request.IssueDate,
                detailsInput
                );

            await _saleRepository.CreateAsync(sale);
            await _unitOfWork.SaveChangesAsync();

            return Result<Guid>.Success(sale.Id);
        }
    }
}
