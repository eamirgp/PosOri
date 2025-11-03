using MediatR;
using Pos.Application.Shared.Pagination;

namespace Pos.Application.Features.Inventory.Queries.GetAllInventories
{
    public record GetAllInventoriesRequest(
        PaginationParams Param,
        Guid warehouseId
        ) : IRequest<PaginatedResult<InventoryDto>>;
}
