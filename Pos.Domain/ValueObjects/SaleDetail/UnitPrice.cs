namespace Pos.Domain.ValueObjects.SaleDetail
{
    public class UnitPrice
    {
        public decimal Value { get; private set; }

        private UnitPrice(decimal value)
        {
            Value = value;
        }

        public static UnitPrice Create(decimal unitPrice)
        {
            if (unitPrice <= 0)
                throw new ArgumentException("El precio unitario debe ser mayor a 0.");

            return new(unitPrice);
        }
    }
}
