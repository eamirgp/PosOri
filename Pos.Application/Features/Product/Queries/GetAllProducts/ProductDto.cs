namespace Pos.Application.Features.Product.Queries.GetAllProducts
{
    public record ProductDto(
        Guid Id,
        Guid UnitOfMeasureId,
        string UnitOfMeasureDescription,
        Guid CategoryId,
        string CategoryName,
        string Code,
        string Name,
        string? Description,
        decimal PurchasePrice,
        decimal SalePrice,
        decimal TotalStock
        );
}
