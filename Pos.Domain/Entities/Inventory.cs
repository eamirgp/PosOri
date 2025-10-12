using Pos.Domain.Entities.Common;
using Pos.Domain.ValueObjects.Inventory;

namespace Pos.Domain.Entities
{
    public class Inventory : BaseEntity
    {
        public Guid ProductId { get; private set; }
        public Guid WarehouseId { get; private set; }
        public Stock Stock { get; private set; } = default!;

        protected Inventory() { }

        private Inventory(Guid productId, Guid warehouseId, Stock stock)
        {
            ProductId = productId;
            WarehouseId = warehouseId;
            Stock = stock;
        }

        public static Inventory Create(Guid productId, Guid warehouseId, decimal stock)
        {
            var stockVO = Stock.Create(stock);

            return new(productId, warehouseId, stockVO);
        }

        public void IncreaseStock(decimal quantity)
        {
            if (quantity < 0)
                throw new ArgumentException("La cantidad debe ser mayor a 0.");

            var newStockValue = Stock.Value + quantity;
            Stock = Stock.Create(newStockValue);
        }
    }
}
