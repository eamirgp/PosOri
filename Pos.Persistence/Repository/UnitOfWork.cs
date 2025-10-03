using Pos.Application.Contracts.Persistence;
using Pos.Persistence.Context;

namespace Pos.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PosDbContext _posDbContext;

        public UnitOfWork(PosDbContext posDbContext)
        {
            _posDbContext = posDbContext;
        }

        public async Task SaveChangesAsync()
        {
            await _posDbContext.SaveChangesAsync();
        }
    }
}
