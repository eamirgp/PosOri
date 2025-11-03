using MediatR;
using Pos.Application.Contracts.Persistence.Queries;

namespace Pos.Application.Features.VoucherType.Queries.GetAllVoucherTypesSelect
{
    public class GetAllVoucherTypesSelectRequestHandler : IRequestHandler<GetAllVoucherTypesSelectRequest, List<VoucherTypeSelectDto>>
    {
        private readonly IVoucherTypeQueryRepository _voucherTypeQueryRepository;

        public GetAllVoucherTypesSelectRequestHandler(IVoucherTypeQueryRepository voucherTypeQueryRepository)
        {
            _voucherTypeQueryRepository = voucherTypeQueryRepository;
        }

        public async Task<List<VoucherTypeSelectDto>> Handle(GetAllVoucherTypesSelectRequest request, CancellationToken cancellationToken)
        {
            return await _voucherTypeQueryRepository.GetAllVoucherTypesSelectAsync();
        }
    }
}
