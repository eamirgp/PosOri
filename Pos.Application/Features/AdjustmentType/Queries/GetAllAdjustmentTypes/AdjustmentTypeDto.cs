namespace Pos.Application.Features.AdjustmentType.Queries.GetAllAdjustmentTypes
{
    public record AdjustmentTypeDto(
        Guid Id,
        string Code,
        string Description
        );
}
