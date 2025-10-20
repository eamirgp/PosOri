using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Pos.Application.Contracts.Queries;
using Pos.Application.Features.Product.Queries.GetAllProducts;

namespace Pos.Persistence.Repository.Queries
{
    public class ProductQueryRepository : IProductQueryRepository
    {
        private readonly string _connectionString;

        public ProductQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DB")
                ?? throw new InvalidOperationException("Connection string 'DB' not found.");
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            var query = @"
                SELECT
                    p.Id,
                    p.CategoryId,
                    c.Name as CategoryName,
                    p.Code,
                    p.Name,
                    p.Description,
                    p.PurchasePrice,
                    p.SalePrice
                FROM Products p
                INNER JOIN Categories c ON p.CategoryId = c.Id
                ORDER BY p.CreatedDate DESC";

            var products = await connection.QueryAsync<ProductDto>(query);
            return products.ToList();
        }
    }
}
