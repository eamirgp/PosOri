using MediatR;
using Pos.Application.Contracts.Persistence.Queries;
using Pos.Application.Shared.Result;

namespace Pos.Application.Features.Sale.Queries.GetSaleById
{
    public class GetSaleByIdRequestHandler : IRequestHandler<GetSaleByIdRequest, Result<SaleCompleteDto>>
    {
        private readonly ISaleQueryRepository _saleQueryRepository;

        public GetSaleByIdRequestHandler(ISaleQueryRepository saleQueryRepository)
        {
            _saleQueryRepository = saleQueryRepository;
        }

        public async Task<Result<SaleCompleteDto>> Handle(GetSaleByIdRequest request, CancellationToken cancellationToken)
        {
            var sale = await _saleQueryRepository.GetSaleByIdAsync(request.Id);

            if (sale is null)
                return Result<SaleCompleteDto>.Failure(new List<string> { "" });

            return Result<SaleCompleteDto>.Success(sale);
        }
    }
}
