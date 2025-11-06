using MediatR;
using Pos.Application.Shared.Pagination;

namespace Pos.Application.Features.Purchase.Queries.GetAllPurchases
{
    public record GetAllPurchasesRequest(
        PaginationParams Param,
        Guid? WarehouseId
        ) : IRequest<PaginatedResult<PurchaseDto>>;
}
