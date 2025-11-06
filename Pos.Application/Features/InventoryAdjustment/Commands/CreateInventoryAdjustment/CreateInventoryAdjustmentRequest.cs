using MediatR;
using Pos.Application.Shared.Result;

namespace Pos.Application.Features.InventoryAdjustment.Commands.CreateInventoryAdjustment
{
    public record CreateInventoryAdjustmentRequest(
        Guid WarehouseId,
        Guid InventoryAdjustmentTypeId,
        string Reason,
        List<CreateInventoryAdjustmentDetailDto> Details
        ) : IRequest<Result<Guid>>;
}
