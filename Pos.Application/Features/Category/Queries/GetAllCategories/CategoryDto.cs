namespace Pos.Application.Features.Category.Queries.GetAllCategories
{
    public record CategoryDto(
        Guid Id,
        string Name,
        string? Description
        );
}
