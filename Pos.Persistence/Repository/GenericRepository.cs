using Microsoft.EntityFrameworkCore;
using Pos.Application.Contracts.Persistence;
using Pos.Domain.Entities.Common;
using Pos.Persistence.Context;

namespace Pos.Persistence.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly PosDbContext _posDbContext;

        public GenericRepository(PosDbContext posDbContext)
        {
            _posDbContext = posDbContext;
        }

        public async Task CreateAsync(T entity)
        {
            await _posDbContext.Set<T>().AddAsync(entity);
        }

        public async Task CreateRangeAsync(List<T> entities)
        {
            await _posDbContext.Set<T>().AddRangeAsync(entities);
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _posDbContext.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetByIdsAsync(List<Guid> ids)
        {
            return await _posDbContext.Set<T>().Where(t => ids.Contains(t.Id)).ToListAsync();
        }
    }
}
