using MediatR;

namespace Pos.Application.Features.IGVType.Queries.GetAllIGVTypes
{
    public record GetAllIGVTypesRequest() : IRequest<List<IGVTypeDto>>;
}
