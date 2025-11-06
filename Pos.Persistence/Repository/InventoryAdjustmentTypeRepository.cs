using Pos.Application.Contracts.Persistence;
using Pos.Domain.Entities;
using Pos.Persistence.Context;

namespace Pos.Persistence.Repository
{
    public class InventoryAdjustmentTypeRepository : GenericRepository<InventoryAdjustmentType>, IInventoryAdjustmentTypeRepository
    {
        public InventoryAdjustmentTypeRepository(PosDbContext posDbContext) : base(posDbContext)
        {
        }
    }
}
