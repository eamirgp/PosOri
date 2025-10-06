using Microsoft.EntityFrameworkCore;
using Pos.Application.Contracts.Persistence;
using Pos.Domain.Entities;
using Pos.Persistence.Context;

namespace Pos.Persistence.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(PosDbContext posDbContext) : base(posDbContext)
        {
        }

        public async Task<bool> ExistName(string name, Guid? id = null)
        {
            var query = _posDbContext.Categories.AsQueryable();

            if (id != null)
                query = query.Where(c => c.Id != id);

            return await query.AnyAsync(c => c.Name.Value == name);
        }
    }
}
