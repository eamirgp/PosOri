namespace Pos.Application.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
