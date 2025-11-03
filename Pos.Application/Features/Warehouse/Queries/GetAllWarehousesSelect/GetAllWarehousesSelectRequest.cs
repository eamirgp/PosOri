using MediatR;

namespace Pos.Application.Features.Warehouse.Queries.GetAllWarehousesSelect
{
    public record GetAllWarehousesSelectRequest() : IRequest<List<WarehouseSelectDto>>;
}
