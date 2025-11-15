namespace Pos.Domain.ValueObjects.InventoryMovement
{
    public class MovementQuantity
    {
        public decimal Value { get; private set; }

        private MovementQuantity(decimal value)
        {
            Value = value;
        }

        public static MovementQuantity Create(decimal quantity)
        {
            return new(quantity);
        }
    }
}
    