using Pos.Application.Features.Category.Queries.GetAllCategories;

namespace Pos.Application.Contracts.Persistence.Queries
{
    public interface ICategoryQueryRepository
    {
        Task<List<CategoryDto>> GetAllCategoriesAsync();
    }
}
