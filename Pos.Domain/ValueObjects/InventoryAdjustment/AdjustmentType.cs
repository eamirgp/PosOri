namespace Pos.Domain.ValueObjects.InventoryAdjustment
{
    public class AdjustmentType
    {
        public string Value { get; private set; }

        private AdjustmentType(string value)
        {
            Value = value;
        }

        public static AdjustmentType Create(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("El tipo de ajuste es requerido.");

            var normalizedType = type.ToUpper();

            if (normalizedType != Initial.Value && normalizedType != Positive.Value && normalizedType != Negative.Value)
                throw new ArgumentException($"El tipo de ajuste debe ser: {Initial.Value}, {Positive.Value} o {Negative.Value}.");

            return new(normalizedType);
        }

        public static AdjustmentType Initial => new("INITIAL");
        public static AdjustmentType Positive => new("POSITIVE");
        public static AdjustmentType Negative => new("NEGATIVE");

        public bool IsInitial => Value == Initial.Value;
        public bool IsPositive => Value == Positive.Value;
        public bool IsNegative => Value == Negative.Value;
    }
}
