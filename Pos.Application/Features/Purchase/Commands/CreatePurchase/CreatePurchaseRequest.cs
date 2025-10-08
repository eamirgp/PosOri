using MediatR;
using Pos.Application.Shared.Result;

namespace Pos.Application.Features.Purchase.Commands.CreatePurchase
{
    public record CreatePurchaseRequest(
        Guid WarehouseId,
        Guid VoucherTypeId,
        Guid CurrencyId,
        Guid PersonId,
        string Serie,
        string Number,
        DateTime IssueDate,
        List<CreatePurchaseDetailDto> Details
        ) : IRequest<Result<Guid>>;
}
