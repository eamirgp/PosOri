namespace Pos.Application.Features.Person.Queries.GetAllPersons
{
    public record PersonDto(
        Guid Id,
        Guid DocumentTypeId,
        string DocumentType, 
        string DocumentNumber,
        string Name,
        string? Address,
        string? Email,
        string? Phone
        );
}
