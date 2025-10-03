namespace Pos.Domain.ValueObjects.Category
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

            if (value.Length > 100)
                throw new ArgumentException("La descripción no puede exceder los 100 caracteres.");

            return new(value);
        }
    }
}
