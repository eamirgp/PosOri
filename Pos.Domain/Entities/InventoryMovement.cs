using Pos.Domain.Entities.Common;
using Pos.Domain.ValueObjects.InventoryMovement;

namespace Pos.Domain.Entities
{
    public class InventoryMovement : BaseEntity
    {
        public Guid ProductId { get; private set; }
        public Guid WarehouseId { get; private set; }
        public MovementType MovementType { get; private set; } = default!;
        public MovementQuantity Quantity { get; private set; } = default!;
        public Guid? ReferenceId { get; private set; }
        public decimal PreviousStock { get; private set; }
        public decimal NewStock { get; private set; }

        protected InventoryMovement() { }

        private InventoryMovement(Guid productId, Guid warehouseId, MovementType movementType, MovementQuantity quantity, Guid? referenceId, decimal previousStock, decimal newStock)
        {
            ProductId = productId;
            WarehouseId = warehouseId;
            MovementType = movementType;
            Quantity = quantity;
            ReferenceId = referenceId;
            PreviousStock = previousStock;
            NewStock = newStock;
        }

        public static InventoryMovement CreatePurchaseMovement(Guid productId, Guid warehouseId, decimal quantity, Guid purchaseId, decimal previousStock)
        {
            var movementType = MovementType.Purchase();
            var movementQuantity = MovementQuantity.Create(quantity);
            var newStock = previousStock + quantity;

            return new(productId, warehouseId, movementType, movementQuantity, purchaseId, previousStock, newStock);
        }

        public static InventoryMovement CreateSaleMovement(Guid productId, Guid warehouseId, decimal quantity, Guid saleId, decimal previousStock)
        {
            var movementType = MovementType.Sale();
            var movementQuantity = MovementQuantity.Create(-quantity);
            var newStock = previousStock - quantity;

            return new(productId, warehouseId, movementType, movementQuantity, saleId, previousStock, newStock);
        }

        public static InventoryMovement CreateAdjustmentMovement(Guid productId, Guid warehouseId, decimal quantity, Guid AdjustmentId, decimal previousStock, bool isIncrease)
        {
            var movementType = MovementType.Adjsutment();
            var movementQuantity = MovementQuantity.Create(isIncrease ? quantity : -quantity);
            var newStock = isIncrease ? previousStock + quantity : previousStock - quantity;

            return new(productId, warehouseId, movementType, movementQuantity, AdjustmentId, previousStock, newStock);
        }
    }
}
