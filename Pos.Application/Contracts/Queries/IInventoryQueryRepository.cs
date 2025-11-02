using Pos.Application.Features.Inventory.Queries.GetAllInventory;
using Pos.Application.Shared.Pagination;

namespace Pos.Application.Contracts.Queries
{
    public interface IInventoryQueryRepository
    {
        Task<PaginatedResult<InventoryDto>> GetAllInventoriesAsync(PaginationParams param, Guid warehouseId);
    }
}
