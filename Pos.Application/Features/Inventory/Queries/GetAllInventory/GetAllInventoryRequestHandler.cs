using MediatR;
using Pos.Application.Contracts.Queries;
using Pos.Application.Shared.Pagination;

namespace Pos.Application.Features.Inventory.Queries.GetAllInventory
{
    public class GetAllInventoryRequestHandler : IRequestHandler<GetAllInventoryRequest, PaginatedResult<InventoryDto>>
    {
        private readonly IInventoryQueryRepository _inventoryQueryRepository;

        public GetAllInventoryRequestHandler(IInventoryQueryRepository inventoryQueryRepository)
        {
            _inventoryQueryRepository = inventoryQueryRepository;
        }

        public async Task<PaginatedResult<InventoryDto>> Handle(GetAllInventoryRequest request, CancellationToken cancellationToken)
        {
            return await _inventoryQueryRepository.GetAllInventoriesAsync(request.Param, request.warehouseId);
        }
    }
}
