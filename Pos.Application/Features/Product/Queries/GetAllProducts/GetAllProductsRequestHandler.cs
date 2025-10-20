using MediatR;
using Pos.Application.Contracts.Queries;

namespace Pos.Application.Features.Product.Queries.GetAllProducts
{
    public class GetAllProductsRequestHandler : IRequestHandler<GetAllProductsRequest, List<ProductDto>>
    {
        private readonly IProductQueryRepository _productQueryRepository;

        public GetAllProductsRequestHandler(IProductQueryRepository productQueryRepository)
        {
            _productQueryRepository = productQueryRepository;
        }

        public async Task<List<ProductDto>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
        {
            var products = await _productQueryRepository.GetAllProductsAsync();
            return products;
        }
    }
}
