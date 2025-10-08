namespace Pos.Domain.ValueObjects.PurchaseDetail
{
    public class Quantity
    {
        public decimal Value { get; private set; }

        private Quantity(decimal value)
        {
            Value = value;
        }

        public static Quantity Create(decimal quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a 0.");

            return new(quantity);
        }
    }
}
