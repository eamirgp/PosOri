namespace Pos.Application.Features.Product.Queries.GetAllProducts
{
    public record ProductDto(
        Guid Id,
        Guid CategoryId,
        string CategoryName,
        string Code,
        string Name,
        string? Description,
        decimal PurchasePrice,
        decimal SalePrice
        );
}
