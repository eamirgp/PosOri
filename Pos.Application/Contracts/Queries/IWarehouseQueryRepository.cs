using Pos.Application.Features.Warehouse.Queries.GetAllWarehouses;

namespace Pos.Application.Contracts.Queries
{
    public interface IWarehouseQueryRepository
    {
        Task<List<WarehouseDto>> GetAllWarehousesAsync();
    }
}
