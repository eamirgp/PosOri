using MediatR;
using Pos.Application.Contracts.Persistence.Queries;
using Pos.Application.Shared.Pagination;

namespace Pos.Application.Features.Product.Queries.GetAllProducts
{
    public class GetAllProductsRequestHandler : IRequestHandler<GetAllProductsRequest, PaginatedResult<ProductDto>>
    {
        private readonly IProductQueryRepository _productQueryRepository;

        public GetAllProductsRequestHandler(IProductQueryRepository productQueryRepository)
        {
            _productQueryRepository = productQueryRepository;
        }

        public async Task<PaginatedResult<ProductDto>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
        {
            var products = await _productQueryRepository.GetAllProductsAsync(request.Param);
            return products;
        }
    }
}
