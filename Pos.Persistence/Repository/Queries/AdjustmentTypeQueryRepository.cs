using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Pos.Application.Contracts.Persistence.Queries;
using Pos.Application.Features.AdjustmentType.Queries.GetAllAdjustmentTypes;

namespace Pos.Persistence.Repository.Queries
{
    public class AdjustmentTypeQueryRepository : IAdjustmentTypeQueryRepository
    {
        private readonly string _connectionString;

        public AdjustmentTypeQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DB")
                ?? throw new InvalidOperationException("");
        }

        public async Task<List<AdjustmentTypeDto>> GetAllAdjustmentTypesAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            var query = @"
                        SELECT
                            Id,
                            Code,
                            Description
                        FROM AdjustmentTypes
                        ORDER BY Description ASC";

            var adjustmentTypes = await connection.QueryAsync<AdjustmentTypeDto>(query);

            return adjustmentTypes.ToList();
        }
    }
}
