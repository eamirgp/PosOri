using MediatR;
using Pos.Application.Contracts.Persistence;

namespace Pos.Application.Features.Product.Commands.CreateProduct
{
    public class CreateProductRequestHandler : IRequestHandler<CreateProductRequest, Guid>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductRequestHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category is null)
                throw new Exception($"La categoría con ID '{request.CategoryId}' no existe.");

            if (await _productRepository.ExistCode(request.Code))
                throw new Exception($"El código '{request.Code}' ya existe.");

            var product = Domain.Entities.Product.Create(
                category.Id,
                request.Code,
                request.Name,
                request.Description,
                request.PurchasePrice,
                request.SalePrice
                );

            await _productRepository.CreateAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return product.Id;
        }
    }
}
