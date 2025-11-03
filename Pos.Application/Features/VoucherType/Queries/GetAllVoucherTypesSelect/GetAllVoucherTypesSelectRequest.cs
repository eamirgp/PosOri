using MediatR;

namespace Pos.Application.Features.VoucherType.Queries.GetAllVoucherTypesSelect
{
    public record GetAllVoucherTypesSelectRequest() : IRequest<List<VoucherTypeSelectDto>>;
}
