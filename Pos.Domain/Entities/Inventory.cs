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

        public static Inventory Create(Guid productId, Guid warehouseId, int stock)
        {
            var stockVO = Stock.Create(stock);

            return new(productId, warehouseId, stockVO);
        }
    }
}
