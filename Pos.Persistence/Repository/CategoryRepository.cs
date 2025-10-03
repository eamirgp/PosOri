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
    }
}
