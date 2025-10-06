using Pos.Domain.Entities;

namespace Pos.Application.Contracts.Persistence
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<bool> ExistCode(string code, Guid? id = null);
    }
}
