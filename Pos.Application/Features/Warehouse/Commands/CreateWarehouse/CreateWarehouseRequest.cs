using MediatR;

namespace Pos.Application.Features.Warehouse.Commands.CreateWarehouse
{
    public record CreateWarehouseRequest(
        string Name,
        string? Address
        ) : IRequest<Guid>;
}
