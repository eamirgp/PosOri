using MediatR;
using Pos.Application.Shared.Pagination;

namespace Pos.Application.Features.InventoryAdjustment.Queries.GetAllInventoryAdjustments
{
    public record GetAllInventoryAdjustmentsRequest(
        PaginationParams param,
        Guid? warehouseId
        ) : IRequest<PaginatedResult<InventoryAdjustmentDto>>;
}
