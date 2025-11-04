using Pos.Application.Features.InventoryAdjustment.Queries.GetAllInventoryAdjustments;
using Pos.Application.Shared.Pagination;

namespace Pos.Application.Contracts.Persistence.Queries
{
    public interface IInventoryAdjustmentQueryRepository
    {
        Task<PaginatedResult<InventoryAdjustmentDto>> GetAllInventoryAdjustmentsAsync(PaginationParams param, Guid? warehouseId);
    }
}
