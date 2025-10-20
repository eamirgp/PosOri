using Pos.Application.Features.Product.Queries.GetAllProducts;
using Pos.Application.Shared.Pagination;

namespace Pos.Application.Contracts.Queries
{
    public interface IProductQueryRepository
    {
        Task<PaginatedResult<ProductDto>> GetAllProductsAsync(PaginationParams param);
    }
}
