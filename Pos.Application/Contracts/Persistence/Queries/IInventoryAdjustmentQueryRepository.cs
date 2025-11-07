using Pos.Application.Features.InventoryAdjustment.Queries.GetInventoryAdjustmentById;

namespace Pos.Application.Contracts.Persistence.Queries
{
    public interface IInventoryAdjustmentQueryRepository
    {
        Task<InventoryAdjustmentCompleteDto?> GetInventoryAdjustmentByIdAsync(Guid inventoryAdjustmentId);
    }
}
