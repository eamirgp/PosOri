using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Pos.Application.Contracts.Persistence.Queries;
using Pos.Application.Features.InventoryMovement.Queries.GetAllInventoryMovements;
using Pos.Application.Shared.Pagination;

namespace Pos.Persistence.Repository.Queries
{
    public class InventoryMovementQueryRepository : IInventoryMovementQueryRepository
    {
        private readonly string _conenctionString;

        public InventoryMovementQueryRepository(IConfiguration configuration)
        {
            _conenctionString = configuration.GetConnectionString("DB")
                ?? throw new InvalidOperationException("");
        }

        public async Task<PaginatedResult<InventoryMovementDto>> GetAllInventoryMovementsAsync(PaginationParams param)
        {
            using var connection = new SqlConnection(_conenctionString);

            var offset = (param.PageNumber - 1) * param.PageSize;

            var countQuery = @"SELECT COUNT(*) FROM InventoryMovements";

            var totalCount = await connection.ExecuteScalarAsync<int>(countQuery);

            var query = @"
                        SELECT
                            im.Id,
                            im.ProductId,
                            p.Name AS Product,
                            im.WarehouseId,
                            w.Name AS Warehouse,
                            im.MovementType,
                            im.Quantity,
                            im.PreviousStock,
                            im.NewStock
                        FROM InventoryMovements im
                        INNER JOIN Products p ON im.ProductId = p.Id
                        INNER JOIN Warehouses w On im.WarehouseId = w.Id
                        ORDER BY im.CreatedDate DESC
                        OFFSET @Offset ROWS
                        FETCH NEXT @PageSize ROWS ONLY";

            var inventoryMovements = await connection.QueryAsync<InventoryMovementDto>(query, new { Offset = offset, PageSize = param.PageSize });

            return new PaginatedResult<InventoryMovementDto>
            {
                Items = inventoryMovements.ToList(),
                PageNumber = param.PageNumber,
                PageSize = param.PageSize,
                TotalCount = totalCount
            };
        }
    }
}
