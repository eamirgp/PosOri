using MediatR;
using Pos.Application.Contracts.Persistence;

namespace Pos.Application.Features.Product.Commands.UpdateProduct
{
    public class UpdateProductRequestHandler : IRequestHandler<UpdateProductRequest, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductRequestHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            if (product is null)
                throw new Exception($"El producto con ID '{request.Id}' no existe.");

            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category is null)
                throw new Exception($"La categoría con ID '{request.CategoryId}' no existe.");

            if (await _productRepository.ExistCode(request.Code, product.Id))
                throw new Exception($"El código '{request.Code}' ya existe.");

            product.UpdateCategory(category.Id);
            product.UpdateCode(request.Code);
            product.UpdateName(request.Name);
            product.UpdateDescription(request.Description);
            product.UpdatePurchasePrice(request.PurchasePrice);
            product.UpdateSalePrice(request.SalePrice);

            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
