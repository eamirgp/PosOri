using MediatR;
using Pos.Application.Shared.Result;

namespace Pos.Application.Features.Sale.Commands.CreateSale
{
    public record CreateSaleRequest(
        Guid WarehouseId,
        Guid VoucherTypeId,
        Guid CurrencyId,
        Guid PersonId,
        DateTime IssueDate,
        List<CreateSaleDetailDto> Details
        ) : IRequest<Result<Guid>>;
}
