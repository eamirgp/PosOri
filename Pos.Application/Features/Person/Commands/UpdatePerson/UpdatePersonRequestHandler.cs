using MediatR;
using Pos.Application.Contracts.Persistence;

namespace Pos.Application.Features.Person.Commands.UpdatePerson
{
    public class UpdatePersonRequestHandler : IRequestHandler<UpdatePersonRequest, Unit>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePersonRequestHandler(IPersonRepository personRepository, IDocumentTypeRepository documentTypeRepository, IUnitOfWork unitOfWork)
        {
            _personRepository = personRepository;
            _documentTypeRepository = documentTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdatePersonRequest request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(request.Id);
            if (person is null)
                throw new Exception($"La persona con ID '{request.Id}' no existe.");

            var documentType = await _documentTypeRepository.GetByIdAsync(request.DocumentTypeId);
            if (documentType is null)
                throw new Exception($"El tipo de documento con ID '{request.DocumentTypeId}' no existe.");

            if (await _personRepository.ExistDocumentNumber(request.DocumentNumber, person.Id))
                throw new Exception($"El número de documento '{request.DocumentNumber}' ya existe.");

            person.UpdateDocumentType(documentType);
            person.UpdateDocumentNumber(request.DocumentNumber, documentType);
            person.UpdateName(request.Name);
            person.UpdateAddress(request.Address);
            person.UpdateEmail(request.Email);
            person.UpdatePhone(request.Phone);

            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
