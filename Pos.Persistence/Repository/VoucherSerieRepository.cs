using Microsoft.EntityFrameworkCore;
using Pos.Application.Contracts.Persistence;
using Pos.Domain.Entities;
using Pos.Persistence.Context;

namespace Pos.Persistence.Repository
{
    public class VoucherSerieRepository : GenericRepository<VoucherSerie>, IVoucherSerieRepository
    {
        public VoucherSerieRepository(PosDbContext posDbContext) : base(posDbContext)
        {
        }

        public async Task<VoucherSerie?> GetByVoucherTypeAsync(Guid voucherTypeId)
        {
            return await _posDbContext.VoucherSeries.FirstOrDefaultAsync(vs => vs.VoucherTypeId == voucherTypeId);
        }
    }
}
