namespace Pos.App.Features.Product.Models
{
    public class CreateProductModel
    {
        public Guid UnitOfMeasureId { get; set; }
        public Guid IGVTypeId { get; set; }
        public Guid CategoryId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
    }
}
