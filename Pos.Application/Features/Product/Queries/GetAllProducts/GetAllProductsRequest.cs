using MediatR;
using Pos.Application.Shared.Pagination;

namespace Pos.Application.Features.Product.Queries.GetAllProducts
{
    public record GetAllProductsRequest(
        PaginationParams Param
        ) : IRequest<PaginatedResult<ProductDto>>;
}
