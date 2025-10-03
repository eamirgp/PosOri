using Pos.Application.Contracts.Persistence;
using Pos.Domain.Entities.Common;
using Pos.Persistence.Context;

namespace Pos.Persistence.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly PosDbContext _posDbContext;

        public GenericRepository(PosDbContext posDbContext)
        {
            _posDbContext = posDbContext;
        }

        public async Task CreateAsync(T entity)
        {
            await _posDbContext.Set<T>().AddAsync(entity);
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _posDbContext.Set<T>().FindAsync(id);
        }
    }
}
