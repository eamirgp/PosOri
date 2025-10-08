using Pos.Domain.Entities.Common;
using Pos.Domain.Inputs;
using Pos.Domain.ValueObjects.Purchase;

namespace Pos.Domain.Entities
{
    public class Purchase : BaseEntity
    {
        public Guid WarehouseId { get; private set; }
        public Guid VoucherTypeId { get; private set; }
        public Guid CurrencyId { get; private set; }
        public Guid PersonId { get; private set; }
        public VoucherNumber VoucherNumber { get; private set; } = default!;
        public IssueDate IssueDate { get; private set; } = default!;
        public decimal SubTotal { get; private set; }
        public decimal Exempt { get; private set; }
        public decimal TaxAmount { get; private set; }
        public decimal Total { get; private set; }

        public List<PurchaseDetail> PurchaseDetails { get; private set; } = new();

        protected Purchase() { }

        private Purchase(Guid warehouseId, Guid voucherTypeId, Guid currencyId, Guid personId, VoucherNumber voucherNumber, IssueDate issueDate)
        {
            WarehouseId = warehouseId;
            VoucherTypeId = voucherTypeId;
            CurrencyId = currencyId;
            PersonId = personId;
            VoucherNumber = voucherNumber;
            IssueDate = issueDate;
            Total = 0;
        }

        public static Purchase Create(Guid warehouseId, Guid voucherTypeId, Guid currencyId, Guid personId, string serie, string number, DateTime issueDate, List<PurchaseDetailInput> details)
        {
            var voucherNumberVO = VoucherNumber.Create(serie, number);
            var issueDateVO = IssueDate.Create(issueDate);

            ValidateHasPurchaseDetail(details);
            ValidateNoDuplicateProducts(details);

            var purchase = new Purchase(warehouseId, voucherTypeId, currencyId, personId, voucherNumberVO, issueDateVO);

            foreach(var detail in details)
            {
                purchase.AddPurchaseDetail(detail.ProductId, detail.UnitOfMeasureId, detail.IGVType, detail.Quantity, detail.UnitValue);
            }

            purchase.CalculateTotal();

            return purchase;
        }

        private void AddPurchaseDetail(Guid productId, Guid unitOfMeasureId, IGVType igvType, decimal quantity, decimal unitValue)
        {
            var detail = PurchaseDetail.Create(this, productId, unitOfMeasureId, igvType, quantity, unitValue);
            PurchaseDetails.Add(detail);
        }

        private void CalculateTotal()
        {
            SubTotal = PurchaseDetails.Where(pd => pd.IGVType.Rate > 0).Sum(pd => pd.Amount);
            Exempt = PurchaseDetails.Where(pd => pd.IGVType.Rate == 0).Sum(pd => pd.Amount);
            TaxAmount = PurchaseDetails.Sum(pd => pd.TaxAmount);
            Total = SubTotal + Exempt + TaxAmount;
        }

        private static void ValidateHasPurchaseDetail(List<PurchaseDetailInput> details)
        {
            if (details is null || !details.Any())
                throw new ArgumentException("La compra debe tener al menos un detalle.");
        }

        private static void ValidateNoDuplicateProducts(List<PurchaseDetailInput> details)
        {
            if (details.Select(d => d.ProductId).Distinct().Count() != details.Count)
                throw new ArgumentException("No puede haber productos duplicados.");
        }
    }
}
