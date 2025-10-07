using Pos.Domain.Entities.Common;

namespace Pos.Domain.Entities
{
    public class Currency : BaseEntity
    {
        public string Code { get; private set; } = default!;
        public string Description { get; private set; } = default!;

        protected Currency() { }
    }
}
