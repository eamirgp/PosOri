using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Pos.Application.Contracts.Queries;
using Pos.Application.Features.Person.Queries.GetAllPersons;
using Pos.Application.Shared.Pagination;

namespace Pos.Persistence.Repository.Queries
{
    public class PersonQueryRepository : IPersonQueryRepository
    {
        private readonly string _connectionString;

        public PersonQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DB")
                ?? throw new InvalidOperationException("Connection string 'DB' not found.");
        }

        public async Task<PaginatedResult<PersonDto>> GetAllPersonsAsync(PaginationParams param)
        {
            using var connection = new SqlConnection(_connectionString);

            var offset = (param.PageNumber - 1) * param.PageSize;

            var searchPattern = $"%{param.SearchTerm}%";
            var startsWithPattern = $"{param.SearchTerm}%";

            var whereClause = string.IsNullOrWhiteSpace(param.SearchTerm)
                ? ""
                : "WHERE p.DocumentNumber LIKE @SearchPattern OR p.Name LIKE @SearchPattern";

            var orderByClause = string.IsNullOrWhiteSpace(param.SearchTerm)
                ? "ORDER BY p.CreatedDate DESC"
                : @"ORDER BY
                        CASE
                            WHEN p.DocumentNumber LIKE @StartsWithPattern THEN 0
                            WHEN p.Name LIKE @StartsWithPattern THEN 1
                            ELSE 2
                        END,
                    p.Name ASC";

            var countQuery = $@"SELECT COUNT(*) FROM Persons p {whereClause}";

            var totalCount = await connection.ExecuteScalarAsync<int>(countQuery, new { SearchPattern = searchPattern });

            var query = $@"
                        SELECT
                            p.Id,
                            p.DocumentTypeId,
                            dt.Description AS DocumentType,
                            p.DocumentNumber,
                            p.Name,
                            p.Address,
                            p.Email,
                            p.Phone
                        FROM Persons p
                        INNER JOIN DocumentTypes dt ON p.DocumentTypeId = dt.Id
                        {whereClause}
                        {orderByClause}
                        OFFSET @Offset ROWS
                        FETCH NEXT @PageSize ROWS ONLY";

            var persons = await connection.QueryAsync<PersonDto>(query, new { Offset = offset, PageSize = param.PageSize, SearchPattern = searchPattern, StartsWithPattern = startsWithPattern });

            return new PaginatedResult<PersonDto>
            {
                Items = persons.ToList(),
                PageNumber = param.PageNumber,
                PageSize = param.PageSize,
                TotalCount = totalCount
            };
        }
    }
}
