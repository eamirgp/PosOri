namespace Pos.Application.Contracts.Persistence
{
    public interface IGenericRepository<T>
    {
        Task CreateAsync(T entity);

        Task<T?> GetByIdAsync(Guid id);
    }
}
