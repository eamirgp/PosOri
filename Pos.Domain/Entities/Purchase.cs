using Pos.Domain.Entities.Common;
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

        public List<PurchaseDetail> PurchaseDetails { get; private set; } = default!;

        protected Purchase() { }

        private Purchase(Guid warehouseId, Guid voucherTypeId, Guid currencyId, Guid personId, VoucherNumber voucherNumber, IssueDate issueDate)
        {
            WarehouseId = warehouseId;
            VoucherTypeId = voucherTypeId;
            CurrencyId = currencyId;
            PersonId = personId;
            VoucherNumber = voucherNumber;
            IssueDate = issueDate;
        }

        public static Purchase Create(Guid warehouseId, Guid voucherTypeId, Guid currencyId, Guid personId, string serie, string number, DateTime issueDate)
        {
            var voucherNumberVO = VoucherNumber.Create(serie, number);
            var issueDateVO = IssueDate.Create(issueDate);

            PurchaseDetail.Create();

            return new(warehouseId, voucherTypeId, currencyId, personId, voucherNumberVO, issueDateVO);
        }
    }
}
