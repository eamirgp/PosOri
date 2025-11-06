namespace Pos.Domain.ValueObjects.InventoryAdjustment
{
    public class Reason
    {
        public string Value { get; private set; }

        public Reason(string value)
        {
            Value = value;
        }

        public static Reason Create(string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
                throw new ArgumentException("El motivo de ajuste es requerido.");

            if (reason.Length > 200)
                throw new ArgumentException("El motivo no puede exceder los 200 caracteres.");

            return new(reason);
        }
    }
}
