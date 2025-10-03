using MediatR;
using Pos.Application.Contracts.Persistence;

namespace Pos.Application.Features.Category.Commands.CreateCategory
{
    public class CreateCategoryRequestHandler : IRequestHandler<CreateCategoryRequest, Guid>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryRequestHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = Domain.Entities.Category.Create(
                request.Name,
                request.Description
                );

            await _categoryRepository.CreateAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return category.Id;
        }
    }
}
