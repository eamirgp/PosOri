using Pos.Domain.Entities.Common;
using Pos.Domain.ValueObjects.Shared;

namespace Pos.Domain.Entities
{
    public class InventoryAdjustmentDetail : BaseEntity
    {
        public Guid InventoryAdjustmentId { get; private set; }
        public Guid ProductId { get; private set; }
        public Guid UnitOfMeasureId { get; private set; }
        public Quantity Quantity { get; private set; } = default!;

        protected InventoryAdjustmentDetail() { }

        private InventoryAdjustmentDetail(Guid inventoryAdjustmentId, Guid productId, Guid unitOfMeasureId, Quantity quantity)
        {
            InventoryAdjustmentId = inventoryAdjustmentId;
            ProductId = productId;
            UnitOfMeasureId = unitOfMeasureId;
            Quantity = quantity;
        }

        public static InventoryAdjustmentDetail Create(Guid inventoryAdjustmentId, Guid productId, Guid unitOfMeasureId, decimal quantity)
        {
            var quantityVO = Quantity.Create(quantity);

            return new(inventoryAdjustmentId, productId, unitOfMeasureId, quantityVO);
        }
    }
}
