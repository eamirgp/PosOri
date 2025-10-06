using Pos.Domain.Entities;

namespace Pos.Application.Contracts.Persistence
{
    public interface IWarehouseRepository : IGenericRepository<Warehouse>
    {
        Task<bool> ExistName(string name, Guid? id = null);
    }
}
