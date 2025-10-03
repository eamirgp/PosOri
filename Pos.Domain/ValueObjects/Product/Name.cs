namespace Pos.Domain.ValueObjects.Product
{
    public class Name
    {
        public string Value {  get; private set; }

        private Name(string value)
        {
            Value = value;
        }

        public static Name Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El nombre es requerido.");

            if (value.Length > 100)
                throw new ArgumentException("El nombre no puede exceder los 100 caracteres.");

            return new(value);
        }
    }
}
