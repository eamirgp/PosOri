using MediatR;

namespace Pos.Application.Features.Category.Commands.UpdateCategory
{
    public record UpdateCategoryRequest(
        Guid Id,
        string Name,
        string? Description
        ) : IRequest<Unit>;
}
