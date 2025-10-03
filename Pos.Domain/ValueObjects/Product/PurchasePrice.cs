namespace Pos.Domain.ValueObjects.Product
{
    public class PurchasePrice
    {
        public decimal Value { get; private set; }

        private PurchasePrice(decimal value)
        {
            Value = value;
        }

        public static PurchasePrice Create(decimal value)
        {
            if (value < 0)
                throw new ArgumentException("El precio de compra debe ser mayor o igual a 0.");

            return new(value);
        }
    }
}
