namespace Pos.Domain.ValueObjects.Inventory
{
    public class Stock
    {
        public decimal Value { get; private set; }

        private Stock(decimal value)
        {
            Value = value;
        }

        public static Stock Create(decimal value)
        {
            if (value < 0)
                throw new ArgumentException("El stock debe ser mayor a 0.");

            return new(value);
        }
    }
}
