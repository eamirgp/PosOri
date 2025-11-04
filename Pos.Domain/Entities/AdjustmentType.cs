using Pos.Domain.Entities.Common;

namespace Pos.Domain.Entities
{
    public class AdjustmentType : BaseEntity
    {
        public string Code { get; private set; } = default!;
        public string Description { get; private set; } = default!;

        protected AdjustmentType() { }
    }
}
