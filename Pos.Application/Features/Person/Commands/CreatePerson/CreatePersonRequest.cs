using MediatR;

namespace Pos.Application.Features.Person.Commands.CreatePerson
{
    public record CreatePersonRequest(
        Guid DocumentTypeId,
        string DocumentNumber,
        string Name,
        string? Address,
        string? Email,
        string? Phone
        ) : IRequest<Guid>;
}
