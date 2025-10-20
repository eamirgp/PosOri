using MediatR;

namespace Pos.Application.Features.Product.Queries.GetAllProducts
{
    public record GetAllProductsRequest() : IRequest<List<ProductDto>>;
}
