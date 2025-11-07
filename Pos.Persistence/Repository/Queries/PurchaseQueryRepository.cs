using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Pos.Application.Contracts.Persistence.Queries;
using Pos.Application.Features.Purchase.Queries.GetAllPurchases;
using Pos.Application.Features.Purchase.Queries.GetPurchaseById;
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

        public async Task<PurchaseCompleteDto?> GetPurchaseByIdAsync(Guid purchaseId)
        {
            using var connection = new SqlConnection(_connectionString);

            var headerQuery = @"
                        SELECT
                            p.Id,
                            p.IssueDate,
                            p.CreatedDate,
                            p.WarehouseId,
                            w.Name AS Warehouse,
                            p.VoucherTypeId,
                            vt.Description AS VoucherType,
                            p.Serie,
                            p.Number,
                            p.PersonId,
                            dt.Description AS PersonDocumentType,
                            pe.DocumentNumber AS PersonDocumentNumber,
                            pe.Name AS PersonName,
                            p.CurrencyId,
                            c.Description AS Currency,
                            p.SubTotal,
                            p.Exempt,
                            p.TaxAmount,
                            p.Total
                        FROM Purchases p
                        INNER JOIN Warehouses w ON p.WarehouseId = w.Id
                        INNER JOIN VoucherTypes vt ON p.VoucherTypeId = vt.Id
                        INNER JOIN Persons pe ON p.PersonId = pe.Id
                        INNER JOIN DocumentTypes dt ON pe.DocumentTypeId = dt.Id
                        INNER JOIN Currencies c ON p.CurrencyId = c.Id
                        WHERE p.Id = @PurchaseId";

            var detailsQuery = @"
                        SELECT
                            pd.Id,
                            pd.ProductId,
                            p.Code AS ProductCode,
                            p.Name AS ProductName,
                            pd.UnitOfMeasureId,
                            um.Description AS UnitOfMeasure,
                            pd.IGVTypeId,
                            it.Description AS IGVType,
                            pd.Quantity,
                            pd.UnitValue,
                            pd.Amount,
                            pd.TaxAmount,
                            pd.LineTotal
                        FROM PurchaseDetails pd
                        INNER JOIN Products p ON pd.ProductId = p.Id
                        INNER JOIN UnitOfMeasures um ON pd.UnitOfMeasureId = um.Id
                        INNER JOIN IGVTypes it ON pd.IGVTypeId = it.Id
                        WHERE pd.PurchaseId = @PurchaseId
                        ORDER BY pd.CreatedDate ASC";

            var header = await connection.QueryFirstOrDefaultAsync<PurchaseHeaderTemp>(headerQuery, new { PurchaseId = purchaseId });

            if (header is null)
                return null;

            var details = await connection.QueryAsync<PurchaseDetailItemDto>(detailsQuery, new { PurchaseId = purchaseId });

            return new PurchaseCompleteDto(
                header.Id,
                header.IssueDate,
                header.CreatedDate,
                header.WarehouseId,
                header.Warehouse,
                header.VoucherTypeId,
                header.VoucherType,
                header.Serie,
                header.Number,
                header.PersonId,
                header.PersonDocumentType,
                header.PersonDocumentNumber,
                header.PersonName,
                header.CurrencyId,
                header.Currency,
                header.SubTotal,
                header.Exempt,
                header.TaxAmount,
                header.Total,
                details.ToList()
                );
        }

        private class PurchaseHeaderTemp
        {
            public Guid Id { get; set; }
            public DateTime IssueDate { get; set; }
            public DateTime CreatedDate { get; set; }
            public Guid WarehouseId { get; set; }
            public string Warehouse { get; set; } = default!;
            public Guid VoucherTypeId { get; set; }
            public string VoucherType { get; set; } = default!;
            public string Serie { get; set; } = default!;
            public string Number { get; set; } = default!;
            public Guid PersonId { get; set; }
            public string PersonDocumentType { get; set; } = default!;
            public string PersonDocumentNumber { get; set; } = default!;
            public string PersonName { get; set; } = default!;
            public Guid CurrencyId { get; set; }
            public string Currency { get; set; } = default!;
            public decimal SubTotal { get; set; }
            public decimal Exempt { get; set; }
            public decimal TaxAmount { get; set; }
            public decimal Total { get; set; }
        }
    }
}
