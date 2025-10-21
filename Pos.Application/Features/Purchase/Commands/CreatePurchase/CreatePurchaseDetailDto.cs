namespace Pos.Application.Features.Purchase.Commands.CreatePurchase
{
    public record CreatePurchaseDetailDto(
        Guid ProductId,
        Guid IGVTypeId,
        decimal Quantity,
        decimal UnitValue
        );
}
