namespace Pos.Application.Features.Product.Queries.GetAllProducts
{
    public record ProductDto(
        Guid Id,
        Guid UnitOfMeasureId,
        string UnitOfMeasure,
        Guid IGVTypeId,
        string IGVType,
        Guid CategoryId,
        string Category,
        string Code,
        string Name,
        string? Description,
        decimal PurchasePrice,
        decimal SalePrice
        );
}
