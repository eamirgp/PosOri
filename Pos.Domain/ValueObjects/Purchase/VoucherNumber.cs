using System.Text.RegularExpressions;

namespace Pos.Domain.ValueObjects.Purchase
{
    public class VoucherNumber
    {
        public string Serie { get; private set; } = default!;
        public string Number { get; private set; } = default!;

        private VoucherNumber(string serie, string number)
        {
            Serie = serie;
            Number = number;
        }

        public static VoucherNumber Create(string serie, string number)
        {
            var serieNormalized = ValidateSerie(serie);
            ValidateNumber(number);

            return new(serieNormalized, number);
        }

        private static string ValidateSerie(string serie)
        {
            if (string.IsNullOrWhiteSpace(serie))
                throw new ArgumentException("La serie es requerida.");

            var serieNormalized = serie.ToUpperInvariant();

            if (serieNormalized.Length != 4)
                throw new ArgumentException("La serie debe tener exactamente 4 caracteres");

            return serieNormalized;
        }

        private static void ValidateNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("El número es requerido.");

            if (!Regex.IsMatch(number, "^[0-9]+$"))
                throw new ArgumentException("El número solo puede contener dígitos.");

            if (number.Length > 8)
                throw new ArgumentException("El número no puede exceder los 8 dígitos.");
        }
    }
}
