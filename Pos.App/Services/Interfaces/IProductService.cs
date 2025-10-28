using Pos.App.Features.Product.Models;
using Pos.App.Shared.Pagination;

namespace Pos.App.Services.Interfaces
{
    public interface IProductService
    {
        Task<PaginatedResultModel<ProductListModel>> GetAllProductsAsync(int pageNumber, int pageSize, string? searchTerm);
        Task<bool> CreateProductAsync(CreateProductModel createProductModel);
    }
}
