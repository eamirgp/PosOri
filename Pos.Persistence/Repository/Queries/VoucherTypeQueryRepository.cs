using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Pos.Application.Contracts.Persistence.Queries;
using Pos.Application.Features.VoucherType.Queries.GetAllVoucherTypesSelect;

namespace Pos.Persistence.Repository.Queries
{
    public class VoucherTypeQueryRepository : IVoucherTypeQueryRepository
    {
        private readonly string _connectionString;

        public VoucherTypeQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DB")
                ?? throw new InvalidOperationException("");
        }

        public async Task<List<VoucherTypeSelectDto>> GetAllVoucherTypesSelectAsync()
        {
            var connection = new SqlConnection(_connectionString);

            var query = @"
                        SELECT
                            Id,
                            Description
                        FROM VoucherTypes";

            var voucherTypes = await connection.QueryAsync<VoucherTypeSelectDto>(query);

            return voucherTypes.ToList();
        }
    }
}
