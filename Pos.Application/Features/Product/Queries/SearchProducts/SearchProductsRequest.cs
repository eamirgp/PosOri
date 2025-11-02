using MediatR;

namespace Pos.Application.Features.Product.Queries.SearchProducts
{
    public record SearchProductsRequest(
        string SearchTerm,
        Guid WarehouseId
        ) : IRequest<List<ProductSearchDto>>;
}
