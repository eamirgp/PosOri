using Pos.App.Features.Category.Models;
using Pos.App.Services.Interfaces;
using System.Net.Http.Json;

namespace Pos.App.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryListModel>> GetAllCategoriesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<CategoryListModel>>("api/Category");
            return response ?? new List<CategoryListModel>();
        }
    }
}
