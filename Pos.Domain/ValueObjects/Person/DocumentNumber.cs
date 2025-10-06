using Pos.Domain.Entities;
using System.Text.RegularExpressions;

namespace Pos.Domain.ValueObjects.Person
{
    public class DocumentNumber
    {
        public string Value { get; private set; } = default!;

        private DocumentNumber(string value)
        {
            Value = value;
        }

        public static DocumentNumber Create(string documentNumber, DocumentType documentType)
        {
            if (string.IsNullOrWhiteSpace(documentNumber))
                throw new ArgumentException("El número de documento es requerido.");

            if (!Regex.IsMatch(documentNumber, "^[0-9]+$"))
                throw new ArgumentException("El número de documento solo puede contener dígitos.");

            if (documentNumber.Length != documentType.Length)
                throw new ArgumentException($"El número de documento debe tener exactamente {documentType.Length} dígitos.");

            return new(documentNumber);
        }
    }
}
