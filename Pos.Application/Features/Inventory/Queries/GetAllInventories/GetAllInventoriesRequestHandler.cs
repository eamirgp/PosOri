using MediatR;
using Pos.Application.Contracts.Persistence.Queries;
using Pos.Application.Shared.Pagination;

namespace Pos.Application.Features.Inventory.Queries.GetAllInventories
{
    public class GetAllInventoriesRequestHandler : IRequestHandler<GetAllInventoriesRequest, PaginatedResult<InventoryDto>>
    {
        private readonly IInventoryQueryRepository _inventoryQueryRepository;

        public GetAllInventoriesRequestHandler(IInventoryQueryRepository inventoryQueryRepository)
        {
            _inventoryQueryRepository = inventoryQueryRepository;
        }

        public async Task<PaginatedResult<InventoryDto>> Handle(GetAllInventoriesRequest request, CancellationToken cancellationToken)
        {
            return await _inventoryQueryRepository.GetAllInventoriesAsync(request.Param, request.warehouseId);
        }
    }
}
