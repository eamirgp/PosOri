using Pos.Domain.Entities.Common;
using Pos.Domain.Inputs;
using Pos.Domain.ValueObjects.InventoryAdjustment;

namespace Pos.Domain.Entities
{
    public class InventoryAdjustment : BaseEntity
    {
        public Guid WarehouseId { get; private set; }
        public Guid InventoryAdjustmentTypeId { get; private set; }
        public Reason Reason { get; private set; } = default!;

        public List<InventoryAdjustmentDetail> InventoryAdjustmentDetails { get; private set; } = new();

        protected InventoryAdjustment() { }

        private InventoryAdjustment(Guid warehouseId, Guid inventoryAdjustmentTypeId, Reason reason)
        {
            WarehouseId = warehouseId;
            InventoryAdjustmentTypeId = inventoryAdjustmentTypeId;
            Reason = reason;
        }

        public static InventoryAdjustment Create(Guid warehouseId, Guid inventoryAdjustmentTypeId, string reason, List<InventoryAdjustmentDetailInput> details)
        {
            var reasonVO = Reason.Create(reason);

            var inventoryAdjustment = new InventoryAdjustment(warehouseId, inventoryAdjustmentTypeId, reasonVO);

            foreach(var detail in details)
            {
                inventoryAdjustment.AddAdjustmentDetail(detail.Product, detail.Quantity);
            }

            return inventoryAdjustment;
        }

        private void AddAdjustmentDetail(Product product, decimal quantity)
        {
            var detail = InventoryAdjustmentDetail.Create(this, product, quantity);
            InventoryAdjustmentDetails.Add(detail);
        }
    }
}
