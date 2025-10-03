namespace Pos.Domain.ValueObjects.Product
{
    public class Description
    {
        public string? Value { get; private set; }

        private Description(string? value)
        {
            Value = value;
        }

        public static Description? Create(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            if (value.Length > 200)
                throw new ArgumentException("La descripción no puede exceder los 200 caracteres.");

            return new(value);
        }
    }
}
