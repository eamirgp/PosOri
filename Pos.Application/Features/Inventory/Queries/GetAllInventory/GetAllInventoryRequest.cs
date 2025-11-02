using MediatR;
using Pos.Application.Shared.Pagination;

namespace Pos.Application.Features.Inventory.Queries.GetAllInventory
{
    public record GetAllInventoryRequest(
        PaginationParams Param,
        Guid warehouseId
        ) : IRequest<PaginatedResult<InventoryDto>>;
}
