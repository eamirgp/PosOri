using Pos.Domain.Entities.Common;
using Pos.Domain.ValueObjects.Shared;

namespace Pos.Domain.Entities
{
    public class Sale : BaseEntity
    {
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

        private Sale(Guid voucherTypeId, Guid currencyId, Guid personId, VoucherNumber voucherNumber, IssueDate issueDate)
        {
            VoucherNumber = voucherNumber;
            IssueDate = issueDate;
        }

        public static Sale Create(Guid voucherTypeId, Guid currencyId, Guid personId, string serie, string number, DateTime issueDate)
        {
            var voucherNumberVO = VoucherNumber.Create(serie, number);
            var issueDateVO = IssueDate.Create(issueDate);

            var sale = new Sale(voucherTypeId, currencyId, personId, voucherNumberVO, issueDateVO);

            
        }
    }
}
 