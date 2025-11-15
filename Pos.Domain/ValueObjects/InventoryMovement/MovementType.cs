namespace Pos.Domain.ValueObjects.InventoryMovement
{
    public class MovementType
    {
        public string Value { get; private set; } = default!;

        private MovementType(string value)
        {
            Value = value;
        }

        public static MovementType Purchase() => new("COMPRA");
        public static MovementType Sale() => new("VENTA");
        public static MovementType Adjsutment() => new("AJUSTE");
    }
}
