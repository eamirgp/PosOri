namespace Pos.Domain.ValueObjects.Product
{
    public class SalePrice
    {
        public decimal Value { get; private set; }

        private SalePrice(decimal value)
        {
            Value = value;
        }

        public static SalePrice Create(decimal value)
        {
            if (value < 0)
                throw new ArgumentException("El precio de venta debe ser mayor o igual a 0.");

            return new(value);
        }
    }
}
