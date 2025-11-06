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

        private InventoryAdjustmentDetail(InventoryAdjustment inventoryAdjustment, Product product, Quantity quantity)
        {
            InventoryAdjustmentId = inventoryAdjustment.Id;
            ProductId = product.Id;
            UnitOfMeasureId = product.UnitOfMeasureId;
            Quantity = quantity;
        }

        public static InventoryAdjustmentDetail Create(InventoryAdjustment inventoryAdjustment, Product product, decimal quantity)
        {
            var quantityVO = Quantity.Create(quantity);

            return new(inventoryAdjustment, product, quantityVO);
        }
    }
}
