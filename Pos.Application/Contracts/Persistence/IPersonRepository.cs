using Pos.Domain.Entities;

namespace Pos.Application.Contracts.Persistence
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        Task<bool> ExistDocumentNumber(string documentNumber, Guid? id = null);
    }
}
