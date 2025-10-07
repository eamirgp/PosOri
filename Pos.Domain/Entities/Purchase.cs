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

        protected Purchase() { }
    }
}
