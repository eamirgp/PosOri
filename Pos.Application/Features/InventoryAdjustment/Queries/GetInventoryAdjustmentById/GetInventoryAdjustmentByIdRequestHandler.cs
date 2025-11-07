using MediatR;
using Pos.Application.Contracts.Persistence.Queries;
using Pos.Application.Shared.Result;

namespace Pos.Application.Features.InventoryAdjustment.Queries.GetInventoryAdjustmentById
{
    public class GetInventoryAdjustmentByIdRequestHandler : IRequestHandler<GetInventoryAdjustmentByIdRequest, Result<InventoryAdjustmentCompleteDto>>
    {
        private readonly IInventoryAdjustmentQueryRepository _inventoryAdjustmentQueryRepository;

        public GetInventoryAdjustmentByIdRequestHandler(IInventoryAdjustmentQueryRepository inventoryAdjustmentQueryRepository)
        {
            _inventoryAdjustmentQueryRepository = inventoryAdjustmentQueryRepository;
        }

        public async Task<Result<InventoryAdjustmentCompleteDto>> Handle(GetInventoryAdjustmentByIdRequest request, CancellationToken cancellationToken)
        {
            var inventoryAdjustment = await _inventoryAdjustmentQueryRepository.GetInventoryAdjustmentByIdAsync(request.Id);
            if (inventoryAdjustment is null)
                return Result<InventoryAdjustmentCompleteDto>.Failure(new List<string> { $"El ajuste de inventario con ID '{request.Id}' no existe." }, 404);

            return Result<InventoryAdjustmentCompleteDto>.Success(inventoryAdjustment);
        }
    }
}
