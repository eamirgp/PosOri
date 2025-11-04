using Pos.Domain.Entities.Common;
using Pos.Domain.Inputs;
using Pos.Domain.ValueObjects.InventoryAdjustment;
using Pos.Domain.ValueObjects.Shared;

namespace Pos.Domain.Entities
{
    public class InventoryAdjustment : BaseEntity
    {
        public Guid WarehouseId { get; private set; }
        public AdjustmentType AdjustmentType { get; private set; } = default!;
        public IssueDate Date { get; private set; } = default!;
        public Reason Reason { get; private set; } = default!;

        public List<InventoryAdjustmentDetail> Details { get; private set; } = new();

        protected InventoryAdjustment() { }

        private InventoryAdjustment(Guid warehouseId, AdjustmentType adjustmentType, IssueDate date, Reason reason)
        {
            WarehouseId = warehouseId;
            AdjustmentType = adjustmentType;
            Date = date;
            Reason = reason;
        }

        public static InventoryAdjustment Create(Guid warehouseId, string adjustmentType, DateTime date, string reason, List<InventoryAdjustmentDetailInput> details)
        {
            var adjustmentTypeVO = AdjustmentType.Create(adjustmentType);
            var dateVO = IssueDate.Create(date);
            var reasonVO = Reason.Create(reason);

            ValidateHasDetails(details);
            ValidateNoDuplicateProducts(details);

            var adjustment = new InventoryAdjustment(warehouseId, adjustmentTypeVO, dateVO, reasonVO);

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
