using MediatR;

namespace Pos.Application.Features.DocumentType.Queries.GetAllDocumentTypesSelect
{
    public record GetAllDocumentTypesSelectRequest() : IRequest<List<DocumentTypeSelectDto>>;
}
