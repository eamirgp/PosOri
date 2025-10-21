using MediatR;
using Pos.Application.Contracts.Persistence;
using Pos.Application.Shared.Result;

namespace Pos.Application.Features.Product.Commands.CreateProduct
{
    public class CreateProductRequestHandler : IRequestHandler<CreateProductRequest, Result<Guid>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfMeasureRepository _unitOfMeasureRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductRequestHandler(
            IProductRepository productRepository,
            IUnitOfMeasureRepository unitOfMeasureRepository,
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork
            )
        {
            _productRepository = productRepository;
            _unitOfMeasureRepository = unitOfMeasureRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var unitOfMeasure = await _unitOfMeasureRepository.GetByIdAsync(request.UnitOfMeasureId);
            if (unitOfMeasure is null)
                return Result<Guid>.Failure(new List<string> { $"La unidad de medida con ID '{request.UnitOfMeasureId}' no existe." }, 404);

            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category is null)
                return Result<Guid>.Failure(new List<string> { $"La categoría con ID '{request.CategoryId}' no existe." }, 404);

            if (await _productRepository.ExistCode(request.Code))
                return Result<Guid>.Failure(new List<string> { $"El código '{request.Code}' ya existe." }, 409);

            var product = Domain.Entities.Product.Create(
                unitOfMeasure.Id,
                category.Id,
                request.Code,
                request.Name,
                request.Description,
                request.PurchasePrice,
                request.SalePrice
                );

            await _productRepository.CreateAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return Result<Guid>.Success(product.Id);
        }
    }
}
