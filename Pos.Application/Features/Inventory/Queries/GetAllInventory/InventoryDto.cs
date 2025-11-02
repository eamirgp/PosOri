namespace Pos.Application.Features.Inventory.Queries.GetAllInventory
{
    public record InventoryDto(
        Guid Id,
        Guid ProductId,
        string ProductCode,
        string ProductName,
        Guid UnitOfMeasureId,
        string UnitOfMeasure,
        decimal Stock
        );
}
