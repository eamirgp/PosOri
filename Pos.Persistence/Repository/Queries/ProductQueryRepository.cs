using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Pos.Application.Contracts.Persistence.Queries;
using Pos.Application.Features.Product.Queries.GetAllProducts;
using Pos.Application.Features.Product.Queries.SearchProducts;
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
            var startsWithPattern = $"{param.SearchTerm}%";

            var whereClause = string.IsNullOrWhiteSpace(param.SearchTerm)
                ? ""
                : "WHERE p.Code LIKE @SearchPattern OR p.Name LIKE @SearchPattern";

            var orderByClause = string.IsNullOrWhiteSpace(param.SearchTerm)
                ? "ORDER BY p.CreatedDate DESC"
                : @"ORDER BY
                        CASE
                            WHEN p.Code LIKE @StartsWithPattern THEN 0
                            WHEN p.Name LIKE @StartsWithPattern THEN 1
                            ELSE 2
                        END,
                    p.Name ASC";

            var countQuery = $"SELECT COUNT(*) FROM Products p {whereClause}";
            var totalCount = await connection.ExecuteScalarAsync<int>(countQuery, new { SearchPattern = searchPattern });

            var query = $@"
                SELECT
                    p.Id,
                    p.UnitOfMeasureId,
                    um.Description as UnitOfMeasure,
                    p.IGVTypeId,
                    it.Description as IGVType,
                    p.CategoryId,
                    c.Name as Category,
                    p.Code,
                    p.Name,
                    p.Description,
                    p.PurchasePrice,
                    p.SalePrice
                FROM Products p
                INNER JOIN UnitOfMeasures um ON p.UnitOfMeasureId = um.Id
                INNER JOIN IGVTypes it ON p.IGVTypeId = it.Id
                INNER JOIN Categories c ON p.CategoryId = c.Id
                {whereClause}
                {orderByClause}
                OFFSET @Offset ROWS
                FETCH NEXT @PageSize ROWS ONLY";

            var products = await connection.QueryAsync<ProductDto>(query, new { Offset = offset, PageSize = param.PageSize, SearchPattern = searchPattern, StartsWithPattern = startsWithPattern });

            return new PaginatedResult<ProductDto>
            {
                Items = products.ToList(),
                PageNumber = param.PageNumber,
                PageSize = param.PageSize,
                TotalCount = totalCount
            };
        }

        public async Task<List<ProductSearchDto>> SearchProductsAsync(string searchTerm, Guid warehouseId)
        {
            using var connection = new SqlConnection(_connectionString);

            var searchPattern = $"%{searchTerm}%";
            var startsWithPattern = $"{searchTerm}%";

            var query = @"
                SELECT TOP 15
                    p.Id,
                    p.Code,
                    p.Name,
                    p.UnitOfMeasureId,
                    um.Description as UnitOfMeasure,
                    p.IGVTypeId,
                    it.Description as IGVType,
                    p.PurchasePrice,
                    p.SalePrice,
                    ISNULL(i.Stock, 0) as Stock
                FROM Products p
                INNER JOIN UnitOfMeasures um ON p.UnitOfMeasureId = um.Id
                INNER JOIN IGVTypes it ON p.IGVTypeId = it.Id
                LEFT JOIN Inventories i ON p.Id = i.ProductId AND i.WarehouseId = @WarehouseId
                WHERE p.Code LIKE @SearchPattern OR p.Name LIKE @SearchPattern
                ORDER BY
                    CASE
                        WHEN p.Code LIKE @StartsWithPattern THEN 0
                        WHEN p.Name LIKE @StartsWithPattern THEN 1
                        ELSE 2
                    END,
                p.Name ASC";

            var products = await connection.QueryAsync<ProductSearchDto>(query, new { SearchPattern = searchPattern, WarehouseId = warehouseId, StartsWithPattern = startsWithPattern });

            return products.ToList();
        }
    }
}
