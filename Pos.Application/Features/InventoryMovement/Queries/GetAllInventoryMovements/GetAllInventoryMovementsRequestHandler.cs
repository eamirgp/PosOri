using MediatR;
using Pos.Application.Contracts.Persistence.Queries;
using Pos.Application.Shared.Pagination;

namespace Pos.Application.Features.InventoryMovement.Queries.GetAllInventoryMovements
{
    public class GetAllInventoryMovementsRequestHandler : IRequestHandler<GetAllInventoryMovementsRequest, PaginatedResult<InventoryMovementDto>>
    {
        private readonly IInventoryMovementQueryRepository _inventoryMovementQueryRepository;

        public GetAllInventoryMovementsRequestHandler(IInventoryMovementQueryRepository inventoryMovementQueryRepository)
        {
            _inventoryMovementQueryRepository = inventoryMovementQueryRepository;
        }

        public async Task<PaginatedResult<InventoryMovementDto>> Handle(GetAllInventoryMovementsRequest request, CancellationToken cancellationToken)
        {
            return await _inventoryMovementQueryRepository.GetAllInventoryMovementsAsync(request.param);
        }
    }
}
