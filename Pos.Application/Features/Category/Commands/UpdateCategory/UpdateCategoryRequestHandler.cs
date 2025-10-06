using MediatR;
using Pos.Application.Contracts.Persistence;

namespace Pos.Application.Features.Category.Commands.UpdateCategory
{
    public class UpdateCategoryRequestHandler : IRequestHandler<UpdateCategoryRequest, Unit>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryRequestHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category is null)
                throw new Exception($"La categoría con ID '{request.Id}' no existe.");

            if (await _categoryRepository.ExistName(request.Name, category.Id))
                throw new Exception($"El nombre '{request.Name}' ya existe.");

            category.UpdateName(request.Name);
            category.UpdateDescription(request.Description);

            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
