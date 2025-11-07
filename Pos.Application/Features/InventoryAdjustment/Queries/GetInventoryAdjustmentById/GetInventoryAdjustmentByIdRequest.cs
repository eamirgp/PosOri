using MediatR;
using Pos.Application.Shared.Result;

namespace Pos.Application.Features.InventoryAdjustment.Queries.GetInventoryAdjustmentById
{
    public record GetInventoryAdjustmentByIdRequest(
        Guid Id
        ) : IRequest<Result<InventoryAdjustmentCompleteDto>>;
}
