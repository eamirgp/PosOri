namespace Pos.Domain.ValueObjects.Person
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

            if (name.Length > 150)
                throw new ArgumentException("El nombre no puede exceder los 150 caracteres.");

            return new(name);
        }
    }
}
