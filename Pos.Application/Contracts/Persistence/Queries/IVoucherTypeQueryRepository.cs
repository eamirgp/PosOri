using Pos.Application.Features.VoucherType.Queries.GetAllVoucherTypesSelect;

namespace Pos.Application.Contracts.Persistence.Queries
{
    public interface IVoucherTypeQueryRepository
    {
        Task<List<VoucherTypeSelectDto>> GetAllVoucherTypesSelectAsync();
    }
}
