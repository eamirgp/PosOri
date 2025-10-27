using MediatR;
using Pos.Application.Contracts.Queries;

namespace Pos.Application.Features.Category.Queries.GetAllCategories
{
    public record GetAllCategoriesRequestHandler : IRequestHandler<GetAllCategoriesRequest, List<CategoryDto>>
    {
        private readonly ICategoryQueryRepository _categoryQueryRepository;

        public GetAllCategoriesRequestHandler(ICategoryQueryRepository categoryQueryRepository)
        {
            _categoryQueryRepository = categoryQueryRepository;
        }

        public async Task<List<CategoryDto>> Handle(GetAllCategoriesRequest request, CancellationToken cancellationToken)
        {
            return await _categoryQueryRepository.GetAllCategoriesAsync();
        }
    }
}
