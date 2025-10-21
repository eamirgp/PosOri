using Pos.Domain.Entities.Common;
using Pos.Domain.ValueObjects.PurchaseDetail;
using Pos.Domain.ValueObjects.Shared;

namespace Pos.Domain.Entities
{
    public class PurchaseDetail : BaseEntity
    {
        public Guid PurchaseId { get; private set; }
        public Guid ProductId { get; private set; }
        public Guid UnitOfMeasureId { get; private set; }
        public Guid IGVTypeId { get; private set; }
        public Quantity Quantity { get; private set; } = default!;
        public UnitValue UnitValue { get; private set; } = default!;
        public decimal Amount { get; private set; }
        public decimal TaxAmount { get; private set; }
        public decimal LineTotal { get; private set; }

        public Purchase Purchase { get; private set; } = default!;
        public Product Product { get; private set; } = default!;
        public IGVType IGVType { get; private set; } = default!;

        protected PurchaseDetail() { }

        private PurchaseDetail(Purchase purchase, Product product, IGVType igvType, Quantity quantity, UnitValue unitValue)
        {
            Purchase = purchase;
            PurchaseId = purchase.Id;
            Product = product;
            ProductId = product.Id;
            UnitOfMeasureId = product.UnitOfMeasureId;
            IGVType = igvType;
            IGVTypeId = igvType.Id;
            Quantity = quantity;
            UnitValue = unitValue;
            Amount = CalculateAmount();
            TaxAmount = CalculateTaxAmount(igvType);
            LineTotal = CalculateLineTotal();
        }

        public static PurchaseDetail Create(Purchase purchase, Product product, IGVType igvType, decimal quantity, decimal unitValue)
        {
            var quantityVO = Quantity.Create(quantity);
            var unitValueVO = UnitValue.Create(unitValue);

            return new(purchase, product, igvType, quantityVO, unitValueVO);
        }

        private decimal CalculateAmount()
        {
            return Quantity.Value * UnitValue.Value;
        }

        private decimal CalculateTaxAmount(IGVType igvType)
        {
            return Amount * igvType.Rate;
        }

        private decimal CalculateLineTotal()
        {
            return Amount + TaxAmount;
        }
    }
}
