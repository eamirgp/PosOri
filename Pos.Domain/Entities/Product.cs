using Pos.Domain.Entities.Common;
using Pos.Domain.ValueObjects.Product;

namespace Pos.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Guid UnitOfMeasureId { get; private set; }
        public Guid IGVTypeId { get; private set; }
        public Guid CategoryId { get; private set; }
        public Code Code { get; private set; } = default!;
        public Name Name { get; private set; } = default!;
        public Description? Description { get; private set; }
        public PurchasePrice PurchasePrice { get; private set; } = default!;
        public SalePrice SalePrice { get; private set; } = default!;

        protected Product() { }

        private Product(Guid unitOfMeasureId, Guid igvTypeId, Guid categoryId, Code code, Name name, Description? description, PurchasePrice purchasePrice, SalePrice salePrice)
        {
            UnitOfMeasureId = unitOfMeasureId;
            IGVTypeId = igvTypeId;
            CategoryId = categoryId;
            Code = code;
            Name = name;
            Description = description;
            PurchasePrice = purchasePrice;
            SalePrice = salePrice;
        }

        public static Product Create(Guid unitOfMeasureId, Guid igvTypeId, Guid categoryId, string code, string name, string? description, decimal purchasePrice, decimal salePrice)
        {
            var codeVO = Code.Create(code);
            var nameVO = Name.Create(name);
            var descriptionVO = Description.Create(description);
            var purchasePriceVO = PurchasePrice.Create(purchasePrice);
            var salePriceVO = SalePrice.Create(salePrice);

            return new(unitOfMeasureId, igvTypeId, categoryId, codeVO, nameVO, descriptionVO, purchasePriceVO, salePriceVO);
        }

        public void UpdateUnitOfMeasure(Guid unitOfMeasureId)
        {
            UnitOfMeasureId = unitOfMeasureId;
        }

        public void UpdateIGVType(Guid igvTypeId)
        {
            IGVTypeId = igvTypeId;
        }

        public void UpdateCategory(Guid categoryId)
        {
            CategoryId = categoryId;
        }

        public void UpdateCode(string code)
        {
            Code = Code.Create(code);
        }

        public void UpdateName(string name)
        {
            Name = Name.Create(name);
        }

        public void UpdateDescription(string? description)
        {
            Description = Description.Create(description);
        }

        public void UpdatePurchasePrice(decimal purchasePrice)
        {
            PurchasePrice = PurchasePrice.Create(purchasePrice);
        }

        public void UpdateSalePrice(decimal salePrice)
        {
            SalePrice = SalePrice.Create(salePrice);
        }
    }
}
