using Pos.Domain.Entities;

namespace Pos.Application.Contracts.Persistence
{
    public interface IPurchaseRepository : IGenericRepository<Purchase>
    {
        Task<bool> ExistVoucherNumber(string serie, string number, Guid voucherTypeId, Guid personId);
    }
}
