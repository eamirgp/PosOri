namespace Pos.Application.Features.Purchase.Commands.CreatePurchase
{
    public record PurchaseDetailRequest(
        Guid ProductId,
        Guid UnitOfMeasureId,
        Guid IGVTypeId,
        decimal Quantity,
        decimal UnitValue
        );
}
