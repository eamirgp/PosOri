using MediatR;
using Pos.Application.Contracts.Persistence.Queries;

namespace Pos.Application.Features.DocumentType.Queries.GetAllDocumentTypesSelect
{
    public class GetAllDocumentTypesSelectRequestHandler : IRequestHandler<GetAllDocumentTypesSelectRequest, List<DocumentTypeSelectDto>>
    {
        private readonly IDocumentTypeQueryRepository _documentTypeQueryRepository;

        public GetAllDocumentTypesSelectRequestHandler(IDocumentTypeQueryRepository documentTypeQueryRepository)
        {
            _documentTypeQueryRepository = documentTypeQueryRepository;
        }

        public async Task<List<DocumentTypeSelectDto>> Handle(GetAllDocumentTypesSelectRequest request, CancellationToken cancellationToken)
        {
            return await _documentTypeQueryRepository.GetAllDocumentTypesSelectAsync();
        }
    }
}
