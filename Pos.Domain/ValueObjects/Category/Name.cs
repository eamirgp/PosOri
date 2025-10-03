namespace Pos.Domain.ValueObjects.Category
{
    public class Name
    {
        public string Value { get; private set; }

        private Name(string value)
        {
            Value = value;
        }

        public static Name Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El nombre es requerido.");

            if (value.Length > 50)
                throw new ArgumentException("El nombre no puede exceder los 50 caracteres.");

            return new(value);
        }
    }
}
