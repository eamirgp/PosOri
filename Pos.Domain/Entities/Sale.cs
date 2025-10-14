using Pos.Domain.Entities.Common;
using Pos.Domain.Inputs;
using Pos.Domain.ValueObjects.Shared;

namespace Pos.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public Guid WarehouseId { get; private set; }
        public Guid VoucherTypeId { get; private set; }
        public Guid CurrencyId { get; private set; }
        public Guid PersonId { get; private set; }
        public VoucherNumber VoucherNumber { get; private set; } = default!;
        public IssueDate IssueDate { get; private set; } = default!;
        public decimal SubTotal { get; private set; }
        public decimal TaxAmount { get; private set; }
        public decimal Total { get; private set; }

        public List<SaleDetail> SaleDetails { get; private set; } = new();

        protected Sale() { }

        private Sale(Guid warehouseId, Guid voucherTypeId, Guid currencyId, Guid personId, VoucherNumber voucherNumber, IssueDate issueDate)
        {
            WarehouseId = warehouseId;
            VoucherTypeId = voucherTypeId;
            CurrencyId = currencyId;
            PersonId = personId;
            VoucherNumber = voucherNumber;
            IssueDate = issueDate;
        }

        public static Sale Create(Guid warehouseId, Guid voucherTypeId, Guid currencyId, Guid personId, string serie, string number, DateTime issueDate, List<SaleDetailInput> details)
        {
            var voucherNumberVO = VoucherNumber.Create(serie, number);
            var issueDateVO = IssueDate.Create(issueDate);

            ValidateHasSaleDetail(details);
            ValidateNoDuplicateProducts(details);

            var sale = new Sale(warehouseId, voucherTypeId, currencyId, personId, voucherNumberVO, issueDateVO);

            foreach (var detail in details)
            {
                sale.AddSaleDetail(detail.ProductId, detail.UnitOfMeasureId, detail.IGVType, detail.Quantity, detail.UnitPrice);
            }

            sale.CalculateTotal();

            return sale;
        }

        private void CalculateTotal()
        {
            SubTotal = SaleDetails.Sum(sd => sd.Amount);
            TaxAmount = SaleDetails.Sum(sd => sd.TaxAmount);
            Total = SaleDetails.Sum(sd => sd.LineTotal);
        }

        private void AddSaleDetail(Guid productId, Guid unitOfMeasureId, IGVType igvType, decimal quantity, decimal unitPrice)
        {
            var saleDetail = SaleDetail.Create(this, productId, unitOfMeasureId, igvType, quantity, unitPrice);
            SaleDetails.Add(saleDetail);
        }

        private static void ValidateHasSaleDetail(List<SaleDetailInput> details)
        {
            if (details is null || !details.Any())
                throw new ArgumentException("La venta debe de tener al menos un detalle.");
        }

        private static void ValidateNoDuplicateProducts(List<SaleDetailInput> details)
        {
            if (details.Select(d => d.ProductId).Distinct().Count() != details.Count)
                throw new ArgumentException("No puede haber productos duplicados.");
        }
    }
}
 