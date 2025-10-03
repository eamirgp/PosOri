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
    }
}
