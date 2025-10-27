using MediatR;

namespace Pos.Application.Features.UnitOfMeasure.Queries.GetAllUnitOfMeasures
{
    public record GetAllUnitOfMeasuresRequest() : IRequest<List<UnitOfMeasureDto>>;
}
