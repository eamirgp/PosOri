using Pos.Domain.Entities.Common;
using Pos.Domain.ValueObjects.Warehouse;

namespace Pos.Domain.Entities
{
    public class Warehouse : BaseEntity
    {
        public Name Name { get; private set; } = default!;
        public Address? Address { get; private set; }

        protected Warehouse() { }

        private Warehouse(Name name, Address? address)
        {
            Name = name;
            Address = address;
        }

        public static Warehouse Create(string name, string? address)
        {
            var nameVO = Name.Create(name);
            var addressVO = Address.Create(address);

            return new(nameVO, addressVO);
        }

        public void UpdateName(string name)
        {
            Name = Name.Create(name);
        }

        public void UpdateAddress(string? address)
        {
            Address = Address.Create(address);
        }
    }
}
