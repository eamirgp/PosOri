namespace Pos.App.Features.Product.Models
{
    public record ProductListModel(
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
