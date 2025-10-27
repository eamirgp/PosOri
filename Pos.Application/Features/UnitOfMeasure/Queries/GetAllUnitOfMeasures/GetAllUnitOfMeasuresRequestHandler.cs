using MediatR;
using Pos.Application.Contracts.Queries;

namespace Pos.Application.Features.UnitOfMeasure.Queries.GetAllUnitOfMeasures
{
    public class GetAllUnitOfMeasuresRequestHandler : IRequestHandler<GetAllUnitOfMeasuresRequest, List<UnitOfMeasureDto>>
    {
        private readonly IUnitOfMeasureQueryRepository _unitOfMeasureQueryRepository;

        public GetAllUnitOfMeasuresRequestHandler(IUnitOfMeasureQueryRepository unitOfMeasureQueryRepository)
        {
            _unitOfMeasureQueryRepository = unitOfMeasureQueryRepository;
        }

        public async Task<List<UnitOfMeasureDto>> Handle(GetAllUnitOfMeasuresRequest request, CancellationToken cancellationToken)
        {
            return await _unitOfMeasureQueryRepository.GetAllUnitOfMeasuresAsync();
        }
    }
}
