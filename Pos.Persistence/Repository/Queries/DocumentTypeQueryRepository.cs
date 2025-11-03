using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Pos.Application.Contracts.Persistence.Queries;
using Pos.Application.Features.DocumentType.Queries.GetAllDocumentTypesSelect;

namespace Pos.Persistence.Repository.Queries
{
    public class DocumentTypeQueryRepository : IDocumentTypeQueryRepository
    {
        private readonly string _connectionString;

        public DocumentTypeQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DB")
                ?? throw new InvalidOperationException("");
        }

        public async Task<List<DocumentTypeSelectDto>> GetAllDocumentTypesSelectAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            var query = @"
                        SELECT
                            Id,
                            Description
                        FROM DocumentTypes";

            var documentTypes = await connection.QueryAsync<DocumentTypeSelectDto>(query);

            return documentTypes.ToList();
        }
    }
}
