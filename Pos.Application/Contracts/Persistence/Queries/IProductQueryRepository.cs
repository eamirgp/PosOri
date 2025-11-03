using Pos.Application.Features.Product.Queries.GetAllProducts;
using Pos.Application.Features.Product.Queries.SearchProducts;
using Pos.Application.Shared.Pagination;

namespace Pos.Application.Contracts.Persistence.Queries
{
    public interface IProductQueryRepository
    {
        Task<PaginatedResult<ProductDto>> GetAllProductsAsync(PaginationParams param);
        Task<List<ProductSearchDto>> SearchProductsAsync(string searchTerm, Guid warehouseId);
    }
}
