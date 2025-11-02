namespace Pos.Application.Features.Warehouse.Queries.GetAllWarehouses
{
    public record WarehouseDto(
        Guid Id,
        string Name,
        string? Address
        );
}
