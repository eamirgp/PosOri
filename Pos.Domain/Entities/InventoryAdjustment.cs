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

            ValidateHasPurchaseDetail(details);
            ValidateNoDuplicateProducts(details);

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

        private static void ValidateHasPurchaseDetail(List<InventoryAdjustmentDetailInput> details)
        {
            if (details is null || !details.Any())
                throw new ArgumentException("El ajuste de inventario debe tener al menos un detalle.");
        }

        private static void ValidateNoDuplicateProducts(List<InventoryAdjustmentDetailInput> details)
        {
            if (details.Select(d => d.Product.Id).Distinct().Count() != details.Count)
                throw new ArgumentException("No puede haber productos duplicados.");
        }
    }
}
