namespace Pos.Application.Features.InventoryAdjustment.Queries.GetInventoryAdjustmentById
{
    public record InventoryAdjustmentCompleteDto(
        Guid Id,
        Guid WarehouseId,
        string Warehouse,
        Guid InventoryAdjustmentTypeId,
        string InventoryAdjustmentType,
        string Reason,
        DateTime CreatedDate,
        List<InventoryAdjusmentDetailItemDto> Details
        );

    public record InventoryAdjusmentDetailItemDto(
        Guid id,
        Guid ProductId,
        string ProductCode,
        string ProductName,
        Guid UnitOfMeasureId,
        string UnitOfMeasure,
        decimal Quantity
        );
}
