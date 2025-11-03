using Pos.Application.Features.DocumentType.Queries.GetAllDocumentTypesSelect;

namespace Pos.Application.Contracts.Persistence.Queries
{
    public interface IDocumentTypeQueryRepository
    {
        Task<List<DocumentTypeSelectDto>> GetAllDocumentTypesSelectAsync();
    }
}
