namespace Pos.Application.Features.Product.Queries.SearchProducts
{
    public record ProductSearchDto(
        Guid Id,
        string Code,
        string Name,
        Guid UnitOfMeasureId,
        string UnitOfMeasure,
        Guid IGVTypeId,
        string IGVType,
        decimal PurchasePrice,
        decimal SalePrice,
        decimal? Stock 
        );
}
