using MediatR;
using Pos.Application.Contracts.Persistence.Queries;
using Pos.Application.Shared.Pagination;

namespace Pos.Application.Features.Person.Queries.GetAllPersons
{
    public class GetAllPersonsRequestHandler : IRequestHandler<GetAllPersonsRequest, PaginatedResult<PersonDto>>
    {
        private readonly IPersonQueryRepository _personQueryRepository;

        public GetAllPersonsRequestHandler(IPersonQueryRepository personQueryRepository)
        {
            _personQueryRepository = personQueryRepository;
        }

        public async Task<PaginatedResult<PersonDto>> Handle(GetAllPersonsRequest request, CancellationToken cancellationToken)
        {
            return await _personQueryRepository.GetAllPersonsAsync(request.param);
        }
    }
}
