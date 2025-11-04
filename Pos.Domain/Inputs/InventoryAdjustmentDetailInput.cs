using Pos.Domain.Entities;

namespace Pos.Domain.Inputs
{
    public record InventoryAdjustmentDetailInput(
        Product Product,
        decimal Quantity,
        string? Notes
        );
}
