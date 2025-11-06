using MediatR;
using Pos.Application.Contracts.Persistence.Queries;
using Pos.Application.Shared.Pagination;

namespace Pos.Application.Features.Sale.Queries.GetAllSales
{
    public class GetAllSalesRequestHandler : IRequestHandler<GetAllSalesRequest, PaginatedResult<SaleDto>>
    {
        private readonly ISaleQueryRepository _saleQueryRepository;

        public GetAllSalesRequestHandler(ISaleQueryRepository saleQueryRepository)
        {
            _saleQueryRepository = saleQueryRepository;
        }

        public async Task<PaginatedResult<SaleDto>> Handle(GetAllSalesRequest request, CancellationToken cancellationToken)
        {
            return await _saleQueryRepository.GetAllSalesAsync(request.Param, request.WarehouseId);
        }
    }
}
