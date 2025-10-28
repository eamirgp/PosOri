using Pos.App.Features.Category.Models;

namespace Pos.App.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryListModel>> GetAllCategoriesAsync();
    }
}
