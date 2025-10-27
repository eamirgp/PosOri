using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Pos.Application.Contracts.Queries;
using Pos.Application.Features.UnitOfMeasure.Queries.GetAllUnitOfMeasures;

namespace Pos.Persistence.Repository.Queries
{
    public class UnitOfMeasureQueryRepository : IUnitOfMeasureQueryRepository
    {
        private readonly string _connectionString;

        public UnitOfMeasureQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DB")
                ?? throw new InvalidOperationException("");
        }

        public async Task<List<UnitOfMeasureDto>> GetAllUnitOfMeasuresAsync()
        {
            var connection = new SqlConnection(_connectionString);

            var query = @"
                        SELECT
                            Id,
                            Description
                        FROM UnitOfMeasures
                        ORDER BY Description ASC";

            var unitOfMeasure = await connection.QueryAsync<UnitOfMeasureDto>(query);

            return unitOfMeasure.ToList();
        }
    }
}
