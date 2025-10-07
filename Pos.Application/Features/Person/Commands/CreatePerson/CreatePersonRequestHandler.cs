using MediatR;
using Pos.Application.Contracts.Persistence;

namespace Pos.Application.Features.Person.Commands.CreatePerson
{
    public class CreatePersonRequestHandler : IRequestHandler<CreatePersonRequest, Guid>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePersonRequestHandler(IPersonRepository personRepository, IDocumentTypeRepository documentTypeRepository, IUnitOfWork unitOfWork)
        {
            _personRepository = personRepository;
            _documentTypeRepository = documentTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreatePersonRequest request, CancellationToken cancellationToken)
        {
            var documentType = await _documentTypeRepository.GetByIdAsync(request.DocumentTypeId);
            if (documentType is null)
                throw new Exception($"El tipo de documento con ID '{request.DocumentTypeId}' no existe.");

            if (await _personRepository.ExistDocumentNumber(request.DocumentNumber))
                throw new Exception($"El número de documento '{request.DocumentNumber}' ya existe.");

            var person = Domain.Entities.Person.Create(
                documentType,
                request.DocumentNumber,
                request.Name,
                request.Address,
                request.Email,
                request.Phone
                );

            await _personRepository.CreateAsync(person);
            await _unitOfWork.SaveChangesAsync();

            return person.Id;
        }
    }
}
