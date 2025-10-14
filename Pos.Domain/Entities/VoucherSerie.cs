using Pos.Domain.Entities.Common;

namespace Pos.Domain.Entities
{
    public class VoucherSerie : BaseEntity
    {
        public Guid VoucherTypeId { get; private set; }
        public string Serie { get; private set; } = default!;
        public int CurrentNumber { get; private set; }

        protected VoucherSerie() { }

        public string GetNextNumber()
        {
            CurrentNumber++;
            return CurrentNumber.ToString().PadLeft(8, '0');
        }
    }
}
