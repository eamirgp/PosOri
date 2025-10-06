namespace Pos.Domain.ValueObjects.Person
{
    public class Phone
    {
        public string? Value { get; private set; }

        private Phone(string? value)
        {
            Value = value;
        }

        public static Phone? Create(string? phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return null;

            if (phone.Length > 20)
                throw new ArgumentException("El teléfono no puede exceder los 20 caracteres.");

            return new(phone);
        }
    }
}
