using Pos.Application.Features.Person.Queries.GetAllPersons;
using Pos.Application.Shared.Pagination;

namespace Pos.Application.Contracts.Queries
{
    public interface IPersonQueryRepository
    {
        Task<PaginatedResult<PersonDto>> GetAllPersonsAsync(PaginationParams param);
    }
}
