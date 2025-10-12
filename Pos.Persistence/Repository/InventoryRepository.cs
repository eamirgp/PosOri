using Microsoft.EntityFrameworkCore;
using Pos.Application.Contracts.Persistence;
using Pos.Domain.Entities;
using Pos.Persistence.Context;

namespace Pos.Persistence.Repository
{
    public class InventoryRepository : GenericRepository<Inventory>, IInventoryRepository
    {
        public InventoryRepository(PosDbContext posDbContext) : base(posDbContext)
        {
        }

        public async Task<List<Inventory>> GetByProductsAndWarehouse(List<Guid> productIds, Guid warehouseId)
        {
            return await _posDbContext.Inventories.Where(i => productIds.Contains(i.ProductId) && i.WarehouseId == warehouseId).ToListAsync();
        }
    }
}
