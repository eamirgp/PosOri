using MediatR;

namespace Pos.Application.Features.Warehouse.Queries.GetAllWarehouses
{
    public record GetAllWarehousesRequest() : IRequest<List<WarehouseDto>>;
}
