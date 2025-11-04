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
        public string? Notes { get; private set; }

        public InventoryAdjustment InventoryAdjustment { get; private set; } = default!;
        public Product Product { get; private set; } = default!;

        protected InventoryAdjustmentDetail() { }

        private InventoryAdjustmentDetail(InventoryAdjustment inventoryAdjustment, Product product, Quantity quantity, string? notes)
        {
            InventoryAdjustment = inventoryAdjustment;
            InventoryAdjustmentId = inventoryAdjustment.Id;
            Product = product;
            ProductId = product.Id;
            UnitOfMeasureId = product.UnitOfMeasureId;
            Quantity = quantity;
            Notes = notes?.Trim();
        }

        public static InventoryAdjustmentDetail Create(InventoryAdjustment inventoryAdjustment, Product product, decimal quantity, string? notes)
        {
            var quantityVO = Quantity.Create(quantity);

            return new(inventoryAdjustment, product, quantityVO, notes);
        }
    }
}
