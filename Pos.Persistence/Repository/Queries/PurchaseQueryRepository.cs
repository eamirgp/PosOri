using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Pos.Application.Contracts.Persistence.Queries;
using Pos.Application.Features.Purchase.Queries.GetAllPurchases;
using Pos.Application.Shared.Pagination;

namespace Pos.Persistence.Repository.Queries
{
    public class PurchaseQueryRepository : IPurchaseQueryRepository
    {
        private readonly string _connectionString;

        public PurchaseQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DB")
                ?? throw new InvalidOperationException("");
        }

        public async Task<PaginatedResult<PurchaseDto>> GetAllPurchasesAsync(PaginationParams param, Guid? warehouseId)
        {
            using var connection = new SqlConnection(_connectionString);

            var offset = (param.PageNumber - 1) * param.PageSize;

            var searchPattern = $"%{param.SearchTerm}%";
            var startsWithPattern = $"{param.SearchTerm}%";

            var whereConditions = new List<string>();

            if (warehouseId != null)
                whereConditions.Add("p.WarehouseId = @WarehouseId");

            if (!string.IsNullOrWhiteSpace(param.SearchTerm))
                whereConditions.Add("(p.Number LIKE @SearchPattern OR pe.DocumentNumber LIKE @SearchPattern OR pe.Name LIKE @SearchPattern)");

            var whereClause = whereConditions.Any() ? "WHERE " + string.Join(" AND ", whereConditions) : "";

            var orderByClause = string.IsNullOrWhiteSpace(param.SearchTerm)
                ? "ORDER BY p.CreatedDate DESC"
                : @"ORDER BY
                            CASE
                                WHEN p.Number LIKE @StartsWithPattern THEN 0
                                WHEN pe.DocumentNumber LIKE @StartsWithPattern THEN 1
                                WHEN pe.Name LIKE @StartsWithPattern THEN 2
                                ELSE 3
                            END,
                            p.CreatedDate DESC";

            var countQuery = $@"
                             SELECT
                             COUNT(*)
                             FROM Purchases p
                             INNER JOIN Persons pe ON p.PersonId = pe.Id
                             {whereClause}";

            var totalCount = await connection.ExecuteScalarAsync<int>(countQuery, new { WarehouseId = warehouseId, SearchPattern = searchPattern });

            var query = $@"
                        SELECT
                            p.Id,
                            p.IssueDate,
                            p.WarehouseId,
                            w.Name AS Warehouse,
                            p.VoucherTypeId,
                            vt.Description AS VoucherType,
                            p.Serie,
                            p.Number,
                            p.PersonId,
                            pe.DocumentNumber AS PersonDocumentNumber,
                            pe.Name AS PersonName,
                            p.CurrencyId,
                            c.Description AS Currency,
                            p.Total
                        FROM Purchases p
                        INNER JOIN Warehouses w ON p.WarehouseId = w.Id
                        INNER JOIN VoucherTypes vt ON p.VoucherTypeId = vt.Id
                        INNER JOIN Persons pe ON p.PersonId = pe.Id
                        INNER JOIN Currencies c ON p.CurrencyId = c.Id
                        {whereClause}
                        {orderByClause}
                        OFFSET @Offset ROWS
                        FETCH NEXT @PageSize ROWS ONLY";

            var purchases = await connection.QueryAsync<PurchaseDto>(query, new { Offset = offset, PageSize = param.PageSize, WarehouseId = warehouseId, SearchPattern = searchPattern, StartsWithPattern = startsWithPattern });

            return new PaginatedResult<PurchaseDto>
            {
                Items = purchases.ToList(),
                PageNumber = param.PageNumber,
                PageSize = param.PageSize,
                TotalCount = totalCount
            };
        }
    }
}
