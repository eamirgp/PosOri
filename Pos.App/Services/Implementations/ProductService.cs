using Pos.App.Features.Product.Models;
using Pos.App.Services.Interfaces;
using Pos.App.Shared.Pagination;
using System.Net.Http.Json;

namespace Pos.App.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginatedResultModel<ProductListModel>> GetAllProductsAsync(int pageNumber, int pageSize, string? searchTerm)
        {
            var response = await _httpClient.GetFromJsonAsync<PaginatedResultModel<ProductListModel>>($"api/Product?pageNumber={pageNumber}&pageSize={pageSize}&searchTerm={searchTerm}");
            return response ?? CreatedEmptyResult(pageNumber, pageSize);
        }

        private static PaginatedResultModel<ProductListModel> CreatedEmptyResult(int pageNumber, int pageSize)
        {
            return new(
                new List<ProductListModel>(),
                pageNumber,
                pageSize,
                0,
                0,
                false,
                false
                );
        }

        public async Task<bool> CreateProductAsync(CreateProductModel createProductModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Product", createProductModel);
            return response.IsSuccessStatusCode;
        }
    }
}
