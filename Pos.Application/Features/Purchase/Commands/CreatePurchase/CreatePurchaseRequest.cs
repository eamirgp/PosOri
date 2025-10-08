using MediatR;

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
        List<PurchaseDetailRequest> PurchaseDetails
        ) : IRequest<Guid>;
}
