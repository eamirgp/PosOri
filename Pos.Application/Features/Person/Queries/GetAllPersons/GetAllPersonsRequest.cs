using MediatR;
using Pos.Application.Shared.Pagination;

namespace Pos.Application.Features.Person.Queries.GetAllPersons
{
    public record GetAllPersonsRequest(PaginationParams param) : IRequest<PaginatedResult<PersonDto>>;
}
