using Pos.Domain.Entities.Common;

namespace Pos.Domain.Entities
{
    public class DocumentType : BaseEntity
    {
        public string Code { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public int Length { get; private set; }

        protected DocumentType() { }
    }
}
