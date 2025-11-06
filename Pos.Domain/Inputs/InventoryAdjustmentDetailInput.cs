namespace Pos.Domain.Inputs
{
    public record InventoryAdjustmentDetailInput(
        Guid ProductId,
        Guid UnitOfMeasureId,
        decimal Quantity
        );
}
