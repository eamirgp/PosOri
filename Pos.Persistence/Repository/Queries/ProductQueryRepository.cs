using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Pos.Application.Contracts.Queries;
using Pos.Application.Features.Product.Queries.GetAllProducts;
using Pos.Application.Shared.Pagination;

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

        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(PaginationParams param)
        {
            using var connection = new SqlConnection(_connectionString);

            var offset = (param.PageNumber - 1) * param.PageSize;

            var searchPattern = $"%{param.SearchTerm}%";

            var whereClause = string.IsNullOrWhiteSpace(param.SearchTerm)
                ? ""
                : "WHERE p.Code LIKE @SearchTerm OR p.Name LIKE @SearchTerm";

            var countQuery = $"SELECT COUNT(*) FROM Products p {whereClause}";
            var totalCount = await connection.ExecuteScalarAsync<int>(countQuery, new { SearchTerm = searchPattern });

            var query = $@"
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
                {whereClause}
                ORDER BY p.CreatedDate DESC
                OFFSET @Offset ROWS
                FETCH NEXT @PageSize ROWS ONLY";

            var products = await connection.QueryAsync<ProductDto>(query, new { Offset = offset, PageSize = param.PageSize, SearchTerm = searchPattern });

            return new PaginatedResult<ProductDto>
            {
                Items = products.ToList(),
                PageNumber = param.PageNumber,
                PageSize = param.PageSize,
                TotalCount = totalCount
            };
        }
    }
}
