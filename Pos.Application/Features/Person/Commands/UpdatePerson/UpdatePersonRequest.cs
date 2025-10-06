using MediatR;

namespace Pos.Application.Features.Person.Commands.UpdatePerson
{
    public record UpdatePersonRequest(
        Guid Id,
        Guid DocumentTypeId,
        string DocumentNumber,
        string Name,
        string? Address,
        string? Email,
        string? Phone
        ) : IRequest<Unit>;
}
