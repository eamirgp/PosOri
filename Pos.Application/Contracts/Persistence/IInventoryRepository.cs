using Pos.Domain.Entities;

namespace Pos.Application.Contracts.Persistence
{
    public interface IInventoryRepository : IGenericRepository<Inventory>
    {
        Task<List<Inventory>> GetByProductsAndWarehouse(List<Guid> productIds, Guid warehouseId);
    }
}
