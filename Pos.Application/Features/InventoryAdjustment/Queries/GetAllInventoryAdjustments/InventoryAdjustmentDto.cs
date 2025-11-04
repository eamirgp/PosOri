namespace Pos.Application.Features.InventoryAdjustment.Queries.GetAllInventoryAdjustments
{
    public record InventoryAdjustmentDto(
        Guid Id,
        DateTime Date,
        Guid WarehouseId,
        string Warehouse,
        string AdjustmentType,
        string Reason
        );
}
