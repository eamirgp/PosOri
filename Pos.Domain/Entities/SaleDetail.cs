using Pos.Domain.Entities.Common;
using Pos.Domain.ValueObjects.SaleDetail;
using Pos.Domain.ValueObjects.Shared;

namespace Pos.Domain.Entities
{
    public class SaleDetail : BaseEntity
    {
        public Guid SaleId { get; private set; }
        public Guid ProductId { get; private set; }
        public Guid UnitOfMeasureId { get; private set; }
        public Guid IGVTypeId { get; private set; }
        public Quantity Quantity { get; private set; } = default!;
        public UnitPrice UnitPrice { get; private set; } = default!;
        public decimal Amount { get; private set; }
        public decimal TaxAmount { get; private set; }
        public decimal LineTotal { get; private set; }

        public IGVType IGVType { get; private set; } = default!;

        protected SaleDetail() { }

        private SaleDetail(Sale sale, Product product, IGVType igvType, Quantity quantity, UnitPrice unitPrice)
        {
            SaleId = sale.Id;
            ProductId = product.Id;
            UnitOfMeasureId = product.UnitOfMeasureId;
            IGVType = igvType;
            IGVTypeId = igvType.Id;
            Quantity = quantity;
            UnitPrice = unitPrice;
            LineTotal = CalculateLineTotal();
            TaxAmount = CalculateTaxAmount(igvType);
            Amount = CalculateAmount();
        }

        public static SaleDetail Create(Sale sale, Product product, IGVType igvType, decimal quantity, decimal unitPrice)
        {
            var quantityVO = Quantity.Create(quantity);
            var unitPriceVO = UnitPrice.Create(unitPrice);

            return new(sale, product, igvType, quantityVO, unitPriceVO);
        }

        private decimal CalculateLineTotal()
        {
            return Quantity.Value * UnitPrice.Value;
        }

        private decimal CalculateTaxAmount(IGVType igvType)
        {
            return (LineTotal / (igvType.Rate + 1)) * igvType.Rate;
        }

        private decimal CalculateAmount()
        {
            return LineTotal - TaxAmount;
        }
    }
}
