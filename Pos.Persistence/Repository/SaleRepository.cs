using Pos.Application.Contracts.Persistence;
using Pos.Domain.Entities;
using Pos.Persistence.Context;

namespace Pos.Persistence.Repository
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        public SaleRepository(PosDbContext posDbContext) : base(posDbContext)
        {
        }
    }
}
