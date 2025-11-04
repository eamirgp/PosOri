using Pos.Domain.Entities.Common;
using Pos.Domain.Inputs;
using Pos.Domain.ValueObjects.InventoryAdjustment;
using Pos.Domain.ValueObjects.Shared;

namespace Pos.Domain.Entities
{
    public class InventoryAdjustment : BaseEntity
    {
        public Guid WarehouseId { get; private set; }
        public Guid AdjustmentTypeId { get; private set; }
        public IssueDate Date { get; private set; } = default!;
        public Reason Reason { get; private set; } = default!;

        public AdjustmentType AdjustmentType { get; private set; } = default!;
        public List<InventoryAdjustmentDetail> Details { get; private set; } = new();

        protected InventoryAdjustment() { }

        private InventoryAdjustment(Guid warehouseId, Guid adjustmentTypeId, IssueDate date, Reason reason)
        {
            WarehouseId = warehouseId;
            AdjustmentTypeId = adjustmentTypeId;
            Date = date;
            Reason = reason;
        }

        public static InventoryAdjustment Create(Guid warehouseId, Guid adjustmentTypeId, DateTime date, string reason, List<InventoryAdjustmentDetailInput> details)
        {
            var dateVO = IssueDate.Create(date);
            var reasonVO = Reason.Create(reason);

            ValidateHasDetails(details);
            ValidateNoDuplicateProducts(details);

            var adjustment = new InventoryAdjustment(warehouseId, adjustmentTypeId, dateVO, reasonVO);

            foreach (var detail in details)
            {
                adjustment.AddDetail(detail.Product, detail.Quantity, detail.Notes);
            }

            return adjustment;
        }

        private void AddDetail(Product product, decimal quantity, string? notes)
        {
            var detail = InventoryAdjustmentDetail.Create(this, product, quantity, notes);
            Details.Add(detail);
        }

        private static void ValidateHasDetails(List<InventoryAdjustmentDetailInput> details)
        {
            if (details is null || !details.Any())
                throw new ArgumentException("El ajuste de inventario debe tener al menos un detalle.");
        }

        private static void ValidateNoDuplicateProducts(List<InventoryAdjustmentDetailInput> details)
        {
            if (details.Select(d => d.Product.Id).Distinct().Count() != details.Count)
                throw new ArgumentException("No puede haber productos duplicados en el ajuste.");
        }
    }
}
