namespace Pos.Domain.ValueObjects.Warehouse
{
    public class Name
    {
        public string Value { get; private set; } = default!;

        private Name(string value)
        {
            Value = value;
        }

        public static Name Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre es requerido.");

            if (name.Length > 50)
                throw new ArgumentException("El nombre no debe exceder los 50 caracteres.");

            return new(name);
        }
    }
}
