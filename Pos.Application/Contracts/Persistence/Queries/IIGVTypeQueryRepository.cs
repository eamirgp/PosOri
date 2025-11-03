using Pos.Application.Features.IGVType.Queries.GetAllIGVTypes;

namespace Pos.Application.Contracts.Persistence.Queries
{
    public interface IIGVTypeQueryRepository
    {
        Task<List<IGVTypeDto>> GetAllIGVTypesAsync();
    }
}
