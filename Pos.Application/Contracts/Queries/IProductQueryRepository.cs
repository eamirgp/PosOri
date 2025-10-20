using Pos.Application.Features.Product.Queries.GetAllProducts;

namespace Pos.Application.Contracts.Queries
{
    public interface IProductQueryRepository
    {
        Task<List<ProductDto>> GetAllProductsAsync();
    }
}
