using Pos.Application.Features.UnitOfMeasure.Queries.GetAllUnitOfMeasures;

namespace Pos.Application.Contracts.Persistence.Queries
{
    public interface IUnitOfMeasureQueryRepository
    {
        Task<List<UnitOfMeasureDto>> GetAllUnitOfMeasuresAsync();
    }
}
