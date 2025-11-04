namespace Pos.Domain.ValueObjects.InventoryAdjustment
{
    public class Reason
    {
        public string Value { get; private set; }

        private Reason(string value)
        {
            Value = value;
        }

        public static Reason Create(string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
                throw new ArgumentException("El motivo del ajuste es requerido.");

            if (reason.Length > 500)
                throw new ArgumentException("El motivo no puede exceder 500 caracteres.");

            return new(reason.Trim());
        }
    }
}
