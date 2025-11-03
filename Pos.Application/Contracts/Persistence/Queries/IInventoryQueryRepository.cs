using Pos.Application.Features.Inventory.Queries.GetAllInventories;
using Pos.Application.Shared.Pagination;

namespace Pos.Application.Contracts.Persistence.Queries
{
    public interface IInventoryQueryRepository
    {
        Task<PaginatedResult<InventoryDto>> GetAllInventoriesAsync(PaginationParams param, Guid warehouseId);
    }
}
