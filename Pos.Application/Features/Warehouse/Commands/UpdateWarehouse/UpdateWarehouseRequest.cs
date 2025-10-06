using MediatR;

namespace Pos.Application.Features.Warehouse.Commands.UpdateWarehouse
{
    public record UpdateWarehouseRequest(
        Guid Id,
        string Name,
        string? Address
        ) : IRequest<Unit>;
}
