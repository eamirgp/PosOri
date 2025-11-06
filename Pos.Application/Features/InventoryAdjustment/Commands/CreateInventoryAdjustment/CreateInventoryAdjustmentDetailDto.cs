namespace Pos.Application.Features.InventoryAdjustment.Commands.CreateInventoryAdjustment
{
    public record CreateInventoryAdjustmentDetailDto(
        Guid ProductId,
        decimal Quantity
        );
}
