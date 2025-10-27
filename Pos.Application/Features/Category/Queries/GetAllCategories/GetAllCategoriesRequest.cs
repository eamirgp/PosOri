using MediatR;

namespace Pos.Application.Features.Category.Queries.GetAllCategories
{
    public record GetAllCategoriesRequest() : IRequest<List<CategoryDto>>;
}
