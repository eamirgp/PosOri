using Pos.Application.Features.InventoryMovement.Queries.GetAllInventoryMovements;
using Pos.Application.Shared.Pagination;

namespace Pos.Application.Contracts.Persistence.Queries
{
    public interface IInventoryMovementQueryRepository
    {
        Task<PaginatedResult<InventoryMovementDto>> GetAllInventoryMovementsAsync(PaginationParams param);
    }
}
