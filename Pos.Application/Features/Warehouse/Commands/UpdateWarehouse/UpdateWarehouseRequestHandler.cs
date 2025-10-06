using MediatR;
using Pos.Application.Contracts.Persistence;

namespace Pos.Application.Features.Warehouse.Commands.UpdateWarehouse
{
    public class UpdateWarehouseRequestHandler : IRequestHandler<UpdateWarehouseRequest, Unit>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWarehouseRequestHandler(IWarehouseRepository warehouseRepository, IUnitOfWork unitOfWork)
        {
            _warehouseRepository = warehouseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateWarehouseRequest request, CancellationToken cancellationToken)
        {
            var warehouse = await _warehouseRepository.GetByIdAsync(request.Id);
            if (warehouse is null)
                throw new Exception($"El almacén con ID '{request.Id}' no existe.");

            if (await _warehouseRepository.ExistName(request.Name, warehouse.Id))
                throw new Exception($"El nombre '{request.Name}' ya existe.");

            warehouse.UpdateName(request.Name);
            warehouse.UpdateAddress(request.Address);

            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
