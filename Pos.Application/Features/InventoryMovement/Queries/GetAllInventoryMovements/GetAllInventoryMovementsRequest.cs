using MediatR;
using Pos.Application.Shared.Pagination;

namespace Pos.Application.Features.InventoryMovement.Queries.GetAllInventoryMovements
{
    public record GetAllInventoryMovementsRequest(
        PaginationParams param
        ) : IRequest<PaginatedResult<InventoryMovementDto>>;
}
