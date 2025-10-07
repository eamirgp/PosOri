namespace Pos.Domain.ValueObjects.Inventory
{
    public class Stock
    {
        public int Value { get; private set; }

        private Stock(int value)
        {
            Value = value;
        }

        public static Stock Create(int value)
        {
            return new(value);
        }
    }
}
