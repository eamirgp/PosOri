namespace Pos.Domain.ValueObjects.Product
{
    public class Code
    {
        public string Value { get; private set; }

        private Code(string value)
        {
            Value = value;
        }

        public static Code Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El código es requerido.");

            var valueNormalized = value.ToUpperInvariant();

            if (valueNormalized.Length > 20)
                throw new ArgumentException("El código no puede exceder los 20 caracteres.");

            return new(valueNormalized);
        }
    }
}
