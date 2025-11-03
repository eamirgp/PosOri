using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Pos.Application.Contracts.Queries;
using Pos.Application.Features.Warehouse.Queries.GetAllWarehouses;
using Pos.Application.Features.Warehouse.Queries.GetAllWarehousesSelect;

namespace Pos.Persistence.Repository.Queries
{
    public class WarehouseQueryRepository : IWarehouseQueryRepository
    {
        private readonly string _connectionString;

        public WarehouseQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DB")
                ?? throw new InvalidOperationException("");
        }

        public async Task<List<WarehouseDto>> GetAllWarehousesAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            var query = @"
                        SELECT
                            Id,
                            Name,
                            Address
                        FROM Warehouses
                        ORDER BY Name ASC";

            var warehouses = await connection.QueryAsync<WarehouseDto>(query);

            return warehouses.ToList();
        }

        public async Task<List<WarehouseSelectDto>> GetAllWarehousesSelectAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            var query = @"
                        SELECT
                            Id,
                            Name
                        FROM Warehouses
                        ORDER BY Name ASC";

            var warehouses = await connection.QueryAsync<WarehouseSelectDto>(query);

            return warehouses.ToList();
        }
    }
}
