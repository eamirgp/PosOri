using MediatR;
using Pos.Application.Contracts.Persistence.Queries;
using Pos.Application.Shared.Pagination;

namespace Pos.Application.Features.InventoryAdjustment.Queries.GetAllInventoryAdjustments
{
    public class GetAllInventoryAdjustmentsRequestHandler : IRequestHandler<GetAllInventoryAdjustmentsRequest, PaginatedResult<InventoryAdjustmentDto>>
    {
        private readonly IInventoryAdjustmentQueryRepository _inventoryAdjustmentQueryRepository;

        public GetAllInventoryAdjustmentsRequestHandler(IInventoryAdjustmentQueryRepository inventoryAdjustmentQueryRepository)
        {
            _inventoryAdjustmentQueryRepository = inventoryAdjustmentQueryRepository;
        }

        public async Task<PaginatedResult<InventoryAdjustmentDto>> Handle(GetAllInventoryAdjustmentsRequest request, CancellationToken cancellationToken)
        {
            return await _inventoryAdjustmentQueryRepository.GetAllInventoryAdjustmentsAsync(request.param, request.warehouseId);
        }
    }
}
