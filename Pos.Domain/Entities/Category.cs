using Pos.Domain.Entities.Common;
using Pos.Domain.ValueObjects.Category;

namespace Pos.Domain.Entities
{
    public class Category : BaseEntity
    {
        public Name Name { get; private set; } = default!;
        public Description? Description { get; private set; }

        protected Category() { }

        private Category(Name name, Description? description)
        {
            Name = name;
            Description = description;
        }

        public static Category Create(string name, string? description)
        {
            var nameVO = Name.Create(name);
            var descriptionVO = Description.Create(description);

            return new(nameVO, descriptionVO);
        }

        public void UpdateName(string name)
        {
            Name = Name.Create(name);
        }

        public void UpdateDescription(string? description)
        {
            Description = Description.Create(description);
        }
    }
}
