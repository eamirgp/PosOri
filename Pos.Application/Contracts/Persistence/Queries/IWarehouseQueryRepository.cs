using Pos.Application.Features.Warehouse.Queries.GetAllWarehouses;
using Pos.Application.Features.Warehouse.Queries.GetAllWarehousesSelect;

namespace Pos.Application.Contracts.Persistence.Queries
{
    public interface IWarehouseQueryRepository
    {
        Task<List<WarehouseDto>> GetAllWarehousesAsync();
        Task<List<WarehouseSelectDto>> GetAllWarehousesSelectAsync();
    }
}
