using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Pos.Application.Contracts.Queries;
using Pos.Application.Features.Inventory.Queries.GetAllInventory;
using Pos.Application.Shared.Pagination;

namespace Pos.Persistence.Repository.Queries
{
    public class InventoryQueryRepository : IInventoryQueryRepository
    {
        private readonly string _connectionString;

        public InventoryQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DB")
                ?? throw new InvalidOperationException("Connection string 'DB' not found.");
        }

        public async Task<PaginatedResult<InventoryDto>> GetAllInventoriesAsync(PaginationParams param, Guid warehouseId)
        {
            using var connection = new SqlConnection(_connectionString);

            var offset = (param.PageNumber - 1) * param.PageSize;

            var searchPattern = $"%{param.SearchTerm}%";
            var startsWithPattern = $"{param.SearchTerm}%";

            var whereConditions = new List<string> { "i.WarehouseId = @WarehouseId" };
            if (!string.IsNullOrWhiteSpace(param.SearchTerm))
                whereConditions.Add("p.Code LIKE @SearchPattern OR p.Name LIKE @SearchPattern");

            var whereClause = "WHERE " + string.Join(" AND ", whereConditions);

            var orderByClause = string.IsNullOrWhiteSpace(param.SearchTerm)
                ? "ORDER BY i.CreatedDate DESC"
                : @"ORDER BY
                        CASE
                            WHEN p.Code LIKE @StartsWithPattern THEN 0
                            WHEN p.Name LIKE @StartsWithPattern THEN 1
                            ELSE 2
                        END,
                        p.Name ASC";

            var countQuery = $@"
                                SELECT COUNT(*)
                                FROM Inventories i
                                INNER JOIN Products p ON i.ProductId = p.Id
                                {whereClause}";

            var totalCount = await connection.ExecuteScalarAsync<int>(countQuery, new { SearchPattern = searchPattern, WarehouseId = warehouseId });

            var query = $@"
                        SELECT
                            i.Id,
                            i.ProductId,
                            p.Code AS ProductCode,
                            p.Name AS ProductName,
                            p.UnitOfMeasureId,
                            um.Description AS UnitOfMeasure,
                            i.Stock
                        FROM Inventories i
                        INNER JOIN Products p ON i.ProductId = p.Id
                        INNER JOIN UnitOfMeasures um ON p.UnitOfMeasureId = um.Id
                        {whereClause}
                        {orderByClause}
                        OFFSET @Offset ROWS
                        FETCH NEXT @PageSize ROWS ONLY";

            var inventories = await connection.QueryAsync<InventoryDto>(query, new { Offset = offset, PageSize = param.PageSize, SearchPattern = searchPattern, StartsWithPattern = startsWithPattern, WarehouseId = warehouseId });

            return new PaginatedResult<InventoryDto>
            {
                Items = inventories.ToList(),
                PageNumber = param.PageNumber,
                PageSize = param.PageSize,
                TotalCount = totalCount
            };
        }
    }
}
