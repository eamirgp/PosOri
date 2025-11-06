namespace Pos.Application.Features.InventoryAdjustment.Commands.CreateInventoryAdjustment
{
    public record CreateInventoryAdjustmentDetailDto(
        Guid ProductId,
        Guid UnitOfMeasureId,
        decimal Quantity
        );
}
