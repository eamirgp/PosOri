using Pos.Domain.Entities.Common;

namespace Pos.Domain.Entities
{
    public class IGVType : BaseEntity
    {
        public string Code { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public decimal Rate { get; private set; }

        protected IGVType() { }
    }
}
