using Microsoft.EntityFrameworkCore;
using Pos.Application.Contracts.Persistence;
using Pos.Domain.Entities;
using Pos.Persistence.Context;

namespace Pos.Persistence.Repository
{
    public class PurchaseRepository : GenericRepository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(PosDbContext posDbContext) : base(posDbContext)
        {
        }

        public async Task<bool> ExistVoucherNumber(string serie, string number, Guid voucherTypeId, Guid personId)
        {
            return await _posDbContext.Purchases.AnyAsync(p =>
            p.VoucherNumber.Serie == serie &&
            p.VoucherNumber.Number == number &&
            p.PersonId == personId &&
            p.VoucherTypeId == voucherTypeId);
        }
    }
}
