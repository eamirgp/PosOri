using Microsoft.EntityFrameworkCore;
using Pos.Application.Contracts.Persistence;
using Pos.Domain.Entities;
using Pos.Persistence.Context;

namespace Pos.Persistence.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(PosDbContext posDbContext) : base(posDbContext)
        {
        }

        public async Task<bool> ExistDocumentNumber(string documentNumber, Guid? id = null)
        {
            var query = _posDbContext.Persons.AsQueryable();

            if (id != null)
                query = query.Where(p => p.Id != id);

            return await query.AnyAsync(p => p.DocumentNumber.Value == documentNumber);
        }
    }
}
