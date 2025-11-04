using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Pos.Application.Contracts.Persistence.Queries;
using Pos.Application.Features.Currency.Queries.GetAllCurrenciesSelect;

namespace Pos.Persistence.Repository.Queries
{
    public class CurrencyQueryRepository : ICurrencyQueryRepository
    {
        private readonly string _connectionString;

        public CurrencyQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DB")
                ?? throw new InvalidOperationException("");
        }

        public async Task<List<CurrencySelectDto>> GetAllCurrenciesSelectAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            var query = @"
                        SELECT
                            Id,
                            Description
                        FROM Currencies";

            var currencies = await connection.QueryAsync<CurrencySelectDto>(query);

            return currencies.ToList();
        }
    }
}
