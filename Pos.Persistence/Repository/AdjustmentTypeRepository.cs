using Pos.Application.Contracts.Persistence;
using Pos.Domain.Entities;
using Pos.Persistence.Context;

namespace Pos.Persistence.Repository
{
    public class AdjustmentTypeRepository : GenericRepository<AdjustmentType>, IAdjustmentTypeRepository
    {
        public AdjustmentTypeRepository(PosDbContext posDbContext) : base(posDbContext)
        {
        }
    }
}
