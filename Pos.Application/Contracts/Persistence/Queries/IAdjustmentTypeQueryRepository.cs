using Pos.Application.Features.AdjustmentType.Queries.GetAllAdjustmentTypes;

namespace Pos.Application.Contracts.Persistence.Queries
{
    public interface IAdjustmentTypeQueryRepository
    {
        Task<List<AdjustmentTypeDto>> GetAllAdjustmentTypesAsync();
    }
}
