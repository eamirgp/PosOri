using MediatR;
using Pos.Application.Shared.Pagination;

namespace Pos.Application.Features.Purchase.Queries.GetAllPurchases
{
    public record GetAllPurchasesRequest(
        PaginationParams param,
        Guid? warehouseId
        ) : IRequest<PaginatedResult<PurchaseDto>>;
}
