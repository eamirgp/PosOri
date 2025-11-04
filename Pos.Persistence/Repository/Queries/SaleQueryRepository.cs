using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Pos.Application.Contracts.Persistence.Queries;
using Pos.Application.Features.Sale.Queries.GetAllSales;
using Pos.Application.Shared.Pagination;

namespace Pos.Persistence.Repository.Queries
{
    public class SaleQueryRepository : ISaleQueryRepository
    {
        private readonly string _connectionString;

        public SaleQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DB")
                ?? throw new InvalidOperationException("");
        }

        public async Task<PaginatedResult<SaleDto>> GetAllSalesAsync(PaginationParams param, Guid? warehouseId)
        {
            using var connection = new SqlConnection(_connectionString);

            var offset = (param.PageNumber - 1) * param.PageSize;

            var searchPattern = $"%{param.SearchTerm}%";
            var startsWithPattern = $"{param.SearchTerm}%";

            var whereConditions = new List<string>();

            if (warehouseId != null)
                whereConditions.Add("s.WarehouseId = @WarehouseId");

            if (!string.IsNullOrWhiteSpace(param.SearchTerm))
                whereConditions.Add("(s.Number LIKE @SearchPattern OR p.DocumentNumber LIKE @SearchPattern OR p.Name LIKE @SearchPattern)");

            var whereClause = whereConditions.Any() ? "WHERE " + string.Join(" AND ", whereConditions) : "";

            var orderByClause = string.IsNullOrEmpty(param.SearchTerm)
                ? "ORDER BY s.CreatedDate DESC"
                : @"ORDER BY
                            CASE
                                WHEN s.Number LIKE @StartsWithPattern THEN 0
                                WHEN p.DocumentNumber LIKE @StartsWithPattern THEN 1
                                WHEN p.Name LIKE @StartsWithPattern THEN 2
                                ELSE 3
                            END,
                            s.CreatedDate DESC";

            var countQuery = $@"
                             SELECT
                             COUNT(*)
                             FROM Sales s
                             INNER JOIN Persons p ON s.PersonId = p.Id
                             {whereClause}";

            var totalCount = await connection.ExecuteScalarAsync<int>(countQuery, new { WarehouseId = warehouseId, SearchPattern = searchPattern });

            var query = $@"
                        SELECT
                            s.Id,
                            s.WarehouseId,
                            w.Name AS Warehouse,
                            s.VoucherTypeId,
                            vt.Description AS VoucherType,
                            s.Serie,
                            s.Number,
                            s.PersonId,
                            p.DocumentNumber AS PersonDocumentNumber,
                            p.Name AS PersonName,
                            s.CurrencyId,
                            c.Description AS Currency,
                            s.Total
                        FROM Sales s
                        INNER JOIN Warehouses w ON s.WarehouseId = w.Id
                        INNER JOIN VoucherTypes vt ON s.VoucherTypeId = vt.Id
                        INNER JOIN Persons p ON s.PersonId = p.Id
                        INNER JOIN Currencies c ON s.CurrencyId = c.Id
                        {whereClause}
                        {orderByClause}
                        OFFSET @Offset ROWS
                        FETCH NEXT @PageSize ROWS ONLY";

            var sales = await connection.QueryAsync<SaleDto>(query, new { Offset = offset, PageSize = param.PageSize, WarehouseId = warehouseId, SearchPattern = searchPattern, StartsWithPattern = startsWithPattern });

            return new PaginatedResult<SaleDto>
            {
                Items = sales.ToList(),
                PageNumber = param.PageNumber,
                PageSize = param.PageSize,
                TotalCount = totalCount,
            };
        }
    }
}
