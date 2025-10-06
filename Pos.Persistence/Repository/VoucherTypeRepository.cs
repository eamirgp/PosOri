using Pos.Application.Contracts.Persistence;
using Pos.Domain.Entities;
using Pos.Persistence.Context;

namespace Pos.Persistence.Repository
{
    public class VoucherTypeRepository : GenericRepository<VoucherType>, IVoucherTypeRepository
    {
        public VoucherTypeRepository(PosDbContext posDbContext) : base(posDbContext)
        {
        }
    }
}
