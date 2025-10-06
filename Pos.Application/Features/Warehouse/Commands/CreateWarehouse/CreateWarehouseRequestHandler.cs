using MediatR;
using Pos.Application.Contracts.Persistence;

namespace Pos.Application.Features.Warehouse.Commands.CreateWarehouse
{
    public class CreateWarehouseRequestHandler : IRequestHandler<CreateWarehouseRequest, Guid>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateWarehouseRequestHandler(IWarehouseRepository warehouseRepository, IUnitOfWork unitOfWork)
        {
            _warehouseRepository = warehouseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateWarehouseRequest request, CancellationToken cancellationToken)
        {
            if (await _warehouseRepository.ExistName(request.Name))
                throw new Exception($"El nombre '{request.Name}' ya existe.");

            var warehouse = Domain.Entities.Warehouse.Create(
                request.Name,
                request.Address
                );

            await _warehouseRepository.CreateAsync(warehouse);
            await _unitOfWork.SaveChangesAsync();

            return warehouse.Id;
        }
    }
}
