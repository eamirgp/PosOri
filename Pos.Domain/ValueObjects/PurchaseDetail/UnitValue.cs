namespace Pos.Domain.ValueObjects.PurchaseDetail
{
    public class UnitValue
    {
        public decimal Value { get; private set; }

        private UnitValue(decimal value)
        {
            Value = value;
        }

        public static UnitValue Create(decimal unitValue)
        {
            if (unitValue <= 0)
                throw new ArgumentException("El valor unitario debe ser mayor a 0.");

            return new(unitValue);
        }
    }
}
