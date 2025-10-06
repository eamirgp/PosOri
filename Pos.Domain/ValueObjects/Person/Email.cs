namespace Pos.Domain.ValueObjects.Person
{
    public class Email
    {
        public string? Value { get; private set; }

        private Email(string? value)
        {
            Value = value;
        }

        public static Email? Create(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            if (email.Length > 100)
                throw new ArgumentException("El correo no debe exceder los 100 caracteres.");

            return new(email);
        }
    }
}
