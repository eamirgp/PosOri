using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Pos.Application.Contracts.Persistence.Queries;
using Pos.Application.Features.InventoryAdjustment.Queries.GetInventoryAdjustmentById;

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

        public async Task<InventoryAdjustmentCompleteDto?> GetInventoryAdjustmentByIdAsync(Guid inventoryAdjustmentId)
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = @"
                        SELECT
                            ia.Id,
                            ia.WarehouseId,
                            w.Name AS Warehouse,
                            ia.InventoryAdjustmentTypeId,
                            iat.Description AS InventoryAdjustmentType,
                            ia.Reason,
                            ia.CreatedDate
                        FROM InventoryAdjustments ia
                        INNER JOIN Warehouses w ON ia.WarehouseId = w.Id
                        INNER JOIN InventoryAdjustmentTypes iat ON ia.InventoryAdjustmentTypeId = iat.Id
                        WHERE ia.Id = @InventoryAdjustmentId;

                        SELECT
                            iad.Id,
                            iad.ProductId,
                            p.Code AS ProductCode,
                            p.Name AS ProductName,
                            iad.UnitOfMeasureId,
                            um.Description AS UnitOfMeasure,
                            iad.Quantity
                        FROM InventoryAdjustmentDetails iad
                        INNER JOIN Products p ON iad.ProductId = p.Id
                        INNER JOIN UnitOfMeasures um ON iad.UnitOfMeasureId = um.Id
                        WHERE iad.InventoryAdjustmentId = @InventoryAdjustmentId
                        ORDER BY iad.CreatedDate ASC;";

            var multi = await connection.QueryMultipleAsync(sql, new { InventoryAdjustmentId = inventoryAdjustmentId });

            var header = await multi.ReadFirstOrDefaultAsync<InventoryAdjustmentHeaderTemp>();
            if (header is null)
                return null;

            var details = (await multi.ReadAsync<InventoryAdjusmentDetailItemDto>()).ToList();

            return new InventoryAdjustmentCompleteDto(
                header.Id,
                header.WarehouseId,
                header.Warehouse,
                header.InventoryAdjustmentTypeId,
                header.InventoryAdjustmentType,
                header.Reason,
                header.CreatedDate,
                details
                );
        }

        private class InventoryAdjustmentHeaderTemp
        {
            public Guid Id { get; set; }
            public Guid WarehouseId { get; set; }
            public string Warehouse { get; set; } = default!;
            public Guid InventoryAdjustmentTypeId { get; set; }
            public string InventoryAdjustmentType { get; set; } = default!;
            public string Reason { get; set; } = default!;
            public DateTime CreatedDate { get; set; }
        }
    }
}
