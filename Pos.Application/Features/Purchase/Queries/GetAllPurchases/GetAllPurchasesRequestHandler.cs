using MediatR;
using Pos.Application.Contracts.Persistence.Queries;
using Pos.Application.Shared.Pagination;

namespace Pos.Application.Features.Purchase.Queries.GetAllPurchases
{
    public class GetAllPurchasesRequestHandler : IRequestHandler<GetAllPurchasesRequest, PaginatedResult<PurchaseDto>>
    {
        private readonly IPurchaseQueryRepository _purchaseQueryRepository;

        public GetAllPurchasesRequestHandler(IPurchaseQueryRepository purchaseQueryRepository)
        {
            _purchaseQueryRepository = purchaseQueryRepository;
        }

        public async Task<PaginatedResult<PurchaseDto>> Handle(GetAllPurchasesRequest request, CancellationToken cancellationToken)
        {
            return await _purchaseQueryRepository.GetAllPurchasesAsync(request.param, request.warehouseId);
        }
    }
}
