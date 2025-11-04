using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Pos.Application.Contracts.Persistence.Queries;
using Pos.Application.Features.InventoryAdjustment.Queries.GetAllInventoryAdjustments;
using Pos.Application.Shared.Pagination;

namespace Pos.Persistence.Repository.Queries
{
    public class InventoryAdjustmentQueryRepository : IInventoryAdjustmentQueryRepository
    {
        private readonly string _connectionString;

        public InventoryAdjustmentQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DB")
                ?? throw new InvalidOperationException("");
        }

        public async Task<PaginatedResult<InventoryAdjustmentDto>> GetAllInventoryAdjustmentsAsync(PaginationParams param, Guid? warehouseId)
        {
            using var connection = new SqlConnection(_connectionString);

            var offset = (param.PageNumber - 1) * param.PageSize;

            var searchPattern = $"%{param.SearchTerm}%";

            var whereConditions = new List<string>();

            if (warehouseId != null)
                whereConditions.Add("ia.WarehouseId = @WarehouseId");

            if (!string.IsNullOrWhiteSpace(param.SearchTerm))
                whereConditions.Add("(ia.Reason LIKE @SearchPattern OR ia.AdjustmentType LIKE @SearchPattern)");

            var whereClause = whereConditions.Any() ? "WHERE " + string.Join(" AND ", whereConditions) : "";

            var orderByClause = "ORDER BY ia.Date DESC, ia.CreatedDate DESC";

            var countQuery = $@"
                             SELECT
                             COUNT(*)
                             FROM InventoryAdjustments ia
                             {whereClause}";

            var totalCount = await connection.ExecuteScalarAsync<int>(countQuery, new { WarehouseId = warehouseId, SearchPattern = searchPattern });

            var query = $@"
                        SELECT
                            ia.Id,
                            ia.Date,
                            ia.WarehouseId,
                            w.Name AS Warehouse,
                            ia.AdjustmentType,
                            ia.Reason
                        FROM InventoryAdjustments ia
                        INNER JOIN Warehouses w ON ia.WarehouseId = w.Id
                        {whereClause}
                        {orderByClause}
                        OFFSET @Offset ROWS
                        FETCH NEXT @PageSize ROWS ONLY";

            var adjustments = await connection.QueryAsync<InventoryAdjustmentDto>(query, new { Offset = offset, PageSize = param.PageSize, WarehouseId = warehouseId, SearchPattern = searchPattern });

            return new PaginatedResult<InventoryAdjustmentDto>
            {
                Items = adjustments.ToList(),
                PageNumber = param.PageNumber,
                PageSize = param.PageSize,
                TotalCount = totalCount
            };
        }
    }
}
