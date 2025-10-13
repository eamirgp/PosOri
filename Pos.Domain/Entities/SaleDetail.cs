using Pos.Domain.Entities.Common;
using Pos.Domain.ValueObjects.SaleDetail;
using Pos.Domain.ValueObjects.Shared;

namespace Pos.Domain.Entities
{
    public class SaleDetail : BaseEntity
    {
        public Guid SaleId { get; private set; }
        public Guid WarehouseId { get; private set; }
        public Guid ProductId { get; private set; }
        public Guid UnitOfMeasureId { get; private set; }
        public Guid IGVTypeId { get; private set; }
        public Quantity Quantity { get; private set; } = default!;
        public UnitPrice UnitPrice { get; private set; } = default!;
        public decimal Amount { get; private set; }
        public decimal TaxAmount { get; private set; }
        public decimal LineTotal { get; private set; }

        public Sale Sale { get; private set; } = default!;
        public IGVType IGVType { get; private set; } = default!;

        protected SaleDetail() { }

        private SaleDetail(Sale sale, Guid warehouseId, Guid productId, Guid unitOfMeasureId, IGVType igvType, Quantity quantity, UnitPrice unitPrice)
        {
            Sale = sale;
            SaleId = sale.Id;
            WarehouseId = warehouseId;
            ProductId = productId;
            UnitOfMeasureId = unitOfMeasureId;
            IGVType = igvType;
            IGVTypeId = igvType.Id;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Amount = CalculateAmount();
            TaxAmount = CalculateTaxAmount();
            LineTotal = Amount;
        }

        public static SaleDetail Create(Sale sale, Guid warehouseId, Guid productId, Guid unitOfMeasureId, IGVType igvType, decimal quantity, decimal unitPrice)
        {
            var quantityVO = Quantity.Create(quantity);
            var unitPriceVO = UnitPrice.Create(unitPrice);

            return new(sale, warehouseId, productId, unitOfMeasureId, igvType, quantityVO, unitPriceVO);
        }

        private decimal CalculateAmount()
        {
            return Quantity.Value * UnitPrice.Value;
        }

        private decimal CalculateTaxAmount()
        {
            return (Amount / (IGVType.Rate + 1)) * IGVType.Rate;
        }
    }
}
