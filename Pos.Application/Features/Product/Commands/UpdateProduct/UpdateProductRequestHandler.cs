using MediatR;
using Pos.Application.Contracts.Persistence;
using Pos.Application.Shared.Result;

namespace Pos.Application.Features.Product.Commands.UpdateProduct
{
    public class UpdateProductRequestHandler : IRequestHandler<UpdateProductRequest, Result<Unit>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfMeasureRepository _unitOfMeasureRepository;
        private readonly IIGVTypeRepository _igvTypeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductRequestHandler(
            IProductRepository productRepository,
            IUnitOfMeasureRepository unitOfMeasureRepository,
            IIGVTypeRepository igvTypeRepository,
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork
            )
        {
            _productRepository = productRepository;
            _unitOfMeasureRepository = unitOfMeasureRepository;
            _igvTypeRepository = igvTypeRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            if (product is null)
                return Result<Unit>.Failure(new List<string> { $"El producto con ID '{request.Id}' no existe." }, 404);

            var unitOfMeasure = await _unitOfMeasureRepository.GetByIdAsync(request.UnitOfMeasureId);
            if (unitOfMeasure is null)
                return Result<Unit>.Failure(new List<string> { $"La unidad de medida con ID '{request.UnitOfMeasureId}' no existe" }, 404);

            var igvType = await _igvTypeRepository.GetByIdAsync(request.IGVTypeId);
            if (igvType is null)
                return Result<Unit>.Failure(new List<string> { $"El tipo de IGV con ID '{request.Id}' no existe." }, 404);

            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category is null)
                return Result<Unit>.Failure(new List<string> { $"La categoría con ID '{request.CategoryId}' no existe." }, 404);

            if (await _productRepository.ExistCode(request.Code, product.Id))
                return Result<Unit>.Failure(new List<string> { $"El código '{request.Code}' ya existe." }, 409);

            product.UpdateUnitOfMeasure(unitOfMeasure.Id);
            product.UpdateIGVType(igvType.Id);
            product.UpdateCategory(category.Id);
            product.UpdateCode(request.Code);
            product.UpdateName(request.Name);
            product.UpdateDescription(request.Description);
            product.UpdatePurchasePrice(request.PurchasePrice);
            product.UpdateSalePrice(request.SalePrice);

            await _unitOfWork.SaveChangesAsync();

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
