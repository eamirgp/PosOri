using Microsoft.EntityFrameworkCore;
using Pos.Application.Contracts.Persistence;
using Pos.Domain.Entities;
using Pos.Persistence.Context;

namespace Pos.Persistence.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(PosDbContext posDbContext) : base(posDbContext)
        {
        }

        public async Task<bool> ExistCode(string code, Guid? id = null)
        {
            var query = _posDbContext.Products.AsQueryable();

            if (id != null)
                query = query.Where(p => p.Id != id);

            return await query.AnyAsync(p => p.Code.Value == code);
        }
    }
}
