using MediatR;
using Pos.Application.Contracts.Persistence;
using Pos.Application.Shared.Result;

namespace Pos.Application.Features.InventoryAdjustment.Commands.CreateInventoryAdjustment
{
    public class CreateInventoryAdjustmentRequestHandler : IRequestHandler<CreateInventoryAdjustmentRequest, Result<Guid>>
    {
        private readonly IInventoryAdjustmentRepository _inventoryAdjustmentRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IInventoryAdjustmentTypeRepository _inventoryAdjustmentTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateInventoryAdjustmentRequestHandler(
            IInventoryAdjustmentRepository inventoryAdjustmentRepository,
            IWarehouseRepository warehouseRepository,
            IInventoryAdjustmentTypeRepository inventoryAdjustmentTypeRepository,
            IUnitOfWork unitOfWork
            )
        {
            _warehouseRepository = warehouseRepository;
            _inventoryAdjustmentRepository = inventoryAdjustmentRepository;
            _inventoryAdjustmentTypeRepository = inventoryAdjustmentTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateInventoryAdjustmentRequest request, CancellationToken cancellationToken)
        {
            var warehouse = await _warehouseRepository.GetByIdAsync(request.WarehouseId);
            if (warehouse is null)
                return Result<Guid>.Failure(new List<string> { $"El almacén con ID '{request.WarehouseId}' no existe." }, 404);

            var inventoryAdjustmentType = await _inventoryAdjustmentTypeRepository.GetByIdAsync(request.InventoryAdjustmentTypeId);
            if (inventoryAdjustmentType is null)
                return Result<Guid>.Failure(new List<string> { $"El tipo de ajuste de inventario con ID '{request.InventoryAdjustmentTypeId}' no existe." }, 404);

            var inventoryAdjustment = Domain.Entities.InventoryAdjustment.Create(
                warehouse.Id,
                inventoryAdjustmentType.Id,
                request.Reason,
                request.Details.Select(d => new Domain.Inputs.InventoryAdjustmentDetailInput(d.ProductId, d.UnitOfMeasureId, d.Quantity)).ToList()
                );

            await _inventoryAdjustmentRepository.CreateAsync(inventoryAdjustment);
            await _unitOfWork.SaveChangesAsync();

            return Result<Guid>.Success(inventoryAdjustment.Id);
        }
    }
}
