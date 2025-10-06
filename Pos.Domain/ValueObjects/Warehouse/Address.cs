namespace Pos.Domain.ValueObjects.Warehouse
{
    public class Address
    {
        public string? Value { get; private set; }

        private Address(string? value)
        {
            Value = value;
        }

        public static Address? Create(string? address)
        {
            if (string.IsNullOrWhiteSpace(address))
                return null;

            if (address.Length > 200)
                throw new ArgumentException("La dirección no puede exceder los 200 caracteres.");

            return new(address);
        }
    }
}
