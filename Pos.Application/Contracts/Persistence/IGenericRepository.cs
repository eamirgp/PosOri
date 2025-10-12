namespace Pos.Application.Contracts.Persistence
{
    public interface IGenericRepository<T>
    {
        Task CreateAsync(T entity);
        Task CreateRangeAsync(List<T> entities);

        Task<T?> GetByIdAsync(Guid id);
        Task<List<T>> GetByIdsAsync(List<Guid> ids);
    }
}
