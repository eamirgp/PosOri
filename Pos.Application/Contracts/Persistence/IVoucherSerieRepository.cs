using Pos.Domain.Entities;

namespace Pos.Application.Contracts.Persistence
{
    public interface IVoucherSerieRepository : IGenericRepository<VoucherSerie>
    {
        Task<VoucherSerie?> GetByVoucherTypeAsync(Guid voucherTypeId);
    }
}
