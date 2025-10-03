using MediatR;

namespace Pos.Application.Features.Category.Commands.CreateCategory
{
    public record CreateCategoryRequest(
        string Name,
        string? Description
        ) : IRequest<Guid>;
}
