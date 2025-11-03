using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Pos.Application.Contracts.Persistence.Queries;
using Pos.Application.Features.Category.Queries.GetAllCategories;

namespace Pos.Persistence.Repository.Queries
{
    public class CategoryQueryRepository : ICategoryQueryRepository
    {
        private readonly string _connectionString;

        public CategoryQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DB")
                ?? throw new InvalidOperationException("Connection string 'DB' not found.");
        }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            var query = @"
                        SELECT
                            Id,
                            Name,
                            Description
                        FROM Categories
                        ORDER BY Name ASC";

            var categories = await connection.QueryAsync<CategoryDto>(query);

            return categories.ToList();
        }
    }
}
