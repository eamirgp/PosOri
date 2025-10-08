using Pos.Application.Contracts.Persistence;
using Pos.Domain.Entities;
using Pos.Persistence.Context;

namespace Pos.Persistence.Repository
{
    public class IGVTypeRepository : GenericRepository<IGVType>, IIGVTypeRepository
    {
        public IGVTypeRepository(PosDbContext posDbContext) : base(posDbContext)
        {
        }
    }
}
