using Pos.Application.Contracts.Persistence;
using Pos.Domain.Entities;
using Pos.Persistence.Context;

namespace Pos.Persistence.Repository
{
    public class InventoryAdjustmentRepository : GenericRepository<InventoryAdjustment>, IInventoryAdjustmentRepository
    {
        public InventoryAdjustmentRepository(PosDbContext posDbContext) : base(posDbContext)
        {
        }
    }
}
