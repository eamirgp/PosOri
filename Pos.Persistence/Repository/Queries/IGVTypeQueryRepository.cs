using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Pos.Application.Contracts.Queries;
using Pos.Application.Features.IGVType.Queries.GetAllIGVTypes;

namespace Pos.Persistence.Repository.Queries
{
    public class IGVTypeQueryRepository : IIGVTypeQueryRepository
    {
        private readonly string _connetionString;

        public IGVTypeQueryRepository(IConfiguration configuration)
        {
            _connetionString = configuration.GetConnectionString("DB")
                ?? throw new InvalidOperationException("");
        }

        public async Task<List<IGVTypeDto>> GetAllIGVTypesAsync()
        {
            using var connection = new SqlConnection(_connetionString);

            var query = @"
                        SELECT
                            Id,
                            Description
                        FROM IGVTypes
                        ORDER BY Description ASC";

            var igvTypes = await connection.QueryAsync<IGVTypeDto>(query);

            return igvTypes.ToList();
        }
    }
}
