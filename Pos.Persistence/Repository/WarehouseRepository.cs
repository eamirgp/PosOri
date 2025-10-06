using Microsoft.EntityFrameworkCore;
using Pos.Application.Contracts.Persistence;
using Pos.Domain.Entities;
using Pos.Persistence.Context;

namespace Pos.Persistence.Repository
{
    public class WarehouseRepository : GenericRepository<Warehouse>, IWarehouseRepository
    {
        public WarehouseRepository(PosDbContext posDbContext) : base(posDbContext)
        {
        }

        public async Task<bool> ExistName(string name, Guid? id = null)
        {
            var query = _posDbContext.Warehouses.AsQueryable();

            if (id != null)
                query = query.Where(w => w.Id != id);

            return await query.AnyAsync(w => w.Name.Value == name);
        }
    }
}
