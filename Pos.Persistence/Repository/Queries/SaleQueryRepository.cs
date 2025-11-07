using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Pos.Application.Contracts.Persistence.Queries;
using Pos.Application.Features.Sale.Queries.GetAllSales;
using Pos.Application.Features.Sale.Queries.GetSaleById;
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
                ? "ORDER BY s.IssueDate DESC"
                : @"ORDER BY
                            CASE
                                WHEN s.Number LIKE @StartsWithPattern THEN 0
                                WHEN p.DocumentNumber LIKE @StartsWithPattern THEN 1
                                WHEN p.Name LIKE @StartsWithPattern THEN 2
                                ELSE 3
                            END,
                            s.IssueDate DESC";

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
                            s.IssueDate,
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

        public async Task<SaleCompleteDto?> GetSaleByIdAsync(Guid saleId)
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = @"
                        SELECT
                            s.Id,
                            s.IssueDate,
                            s.CreatedDate,
                            s.WarehouseId,
                            w.Name AS Warehouse,
                            s.VoucherTypeId,
                            vt.Description AS VoucherType,
                            s.Serie,
                            s.Number,
                            s.PersonId,
                            dt.Description AS PersonDocumentType,
                            pe.DocumentNumber AS PersonDocumentNumber,
                            pe.Name AS PersonName,
                            s.CurrencyId,
                            c.Description AS Currency,
                            s.SubTotal,
                            s.TaxAmount,
                            s.Total
                        FROM Sales s
                        INNER JOIN Warehouses w ON s.WarehouseId = w.Id
                        INNER JOIN VoucherTypes vt ON s.VoucherTypeId = vt.Id
                        INNER JOIN Persons pe ON s.PersonId = pe.Id
                        INNER JOIN DocumentTypes dt ON pe.DocumentTypeId = dt.Id
                        INNER JOIN Currencies c ON s.CurrencyId = c.Id
                        WHERE s.Id = @SaleId;

                        SELECT
                            sd.Id,
                            sd.ProductId,
                            p.Code AS ProductCode,
                            p.Name AS ProductName,
                            sd.UnitOfMeasureId,
                            um.Description AS UnitOfMeasure,
                            sd.IGVTypeId,
                            it.Description AS IGVType,
                            sd.Quantity,
                            sd.UnitPrice,
                            sd.Amount,
                            sd.TaxAmount,
                            sd.LineTotal
                        FROM SaleDetails sd
                        INNER JOIN Products p ON sd.ProductId = p.Id
                        INNER JOIN UnitOfMeasures um ON sd.UnitOfMeasureId = um.Id
                        INNER JOIN IGVTypes it ON sd.IGVTypeId = it.Id
                        WHERE sd.SaleId = @SaleId
                        ORDER BY sd.CreatedDate ASC;";

            var multi = await connection.QueryMultipleAsync(sql, new { SaleId = saleId });

            var header = await multi.ReadFirstOrDefaultAsync<SaleHeaderTemp>();
            if(header is null)
                return null;

            var details = (await multi.ReadAsync<SaleDetailItemDto>()).ToList();

            return new SaleCompleteDto(
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
                header.TaxAmount,
                header.Total,
                details
                );
        }

        private class SaleHeaderTemp
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
            public decimal TaxAmount { get; set; }
            public decimal Total { get; set; }
        }
    }
}
