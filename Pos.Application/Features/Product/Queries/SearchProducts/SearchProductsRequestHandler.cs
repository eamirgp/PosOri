using MediatR;
using Pos.Application.Contracts.Persistence.Queries;

namespace Pos.Application.Features.Product.Queries.SearchProducts
{
    public class SearchProductsRequestHandler : IRequestHandler<SearchProductsRequest, List<ProductSearchDto>>
    {
        private readonly IProductQueryRepository _productQueryRepository;

        public SearchProductsRequestHandler(IProductQueryRepository productQueryRepository)
        {
            _productQueryRepository = productQueryRepository;
        }

        public async Task<List<ProductSearchDto>> Handle(SearchProductsRequest request, CancellationToken cancellationToken)
        {
            return await _productQueryRepository.SearchProductsAsync(request.SearchTerm, request.WarehouseId);
        }
    }
}
