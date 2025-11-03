using MediatR;
using Pos.Application.Contracts.Persistence.Queries;

namespace Pos.Application.Features.IGVType.Queries.GetAllIGVTypes
{
    public class GetAllIGVTypesRequestHandler : IRequestHandler<GetAllIGVTypesRequest, List<IGVTypeDto>>
    {
        private readonly IIGVTypeQueryRepository _igvTypeQueryRepository;

        public GetAllIGVTypesRequestHandler(IIGVTypeQueryRepository igvTypeQueryRepository)
        {
            _igvTypeQueryRepository = igvTypeQueryRepository;
        }

        public async Task<List<IGVTypeDto>> Handle(GetAllIGVTypesRequest request, CancellationToken cancellationToken)
        {
            return await _igvTypeQueryRepository.GetAllIGVTypesAsync();
        }
    }
}
