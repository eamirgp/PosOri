namespace Pos.Application.Features.InventoryMovement.Queries.GetAllInventoryMovements
{
    public record InventoryMovementDto(
        Guid Id,
        Guid ProductId,
        string Product,
        Guid WarehouseId,
        string Warehouse,
        string MovementType,
        decimal Quantity,
        decimal PreviousStock,
        decimal NewStock
        );
}
