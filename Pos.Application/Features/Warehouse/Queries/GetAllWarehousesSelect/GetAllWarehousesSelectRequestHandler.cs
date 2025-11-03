using MediatR;
using Pos.Application.Contracts.Queries;

namespace Pos.Application.Features.Warehouse.Queries.GetAllWarehousesSelect
{
    public class GetAllWarehousesSelectRequestHandler : IRequestHandler<GetAllWarehousesSelectRequest, List<WarehouseSelectDto>>
    {
        private readonly IWarehouseQueryRepository _warehouseQueryRepository;

        public GetAllWarehousesSelectRequestHandler(IWarehouseQueryRepository warehouseQueryRepository)
        {
            _warehouseQueryRepository = warehouseQueryRepository;
        }

        public async Task<List<WarehouseSelectDto>> Handle(GetAllWarehousesSelectRequest request, CancellationToken cancellationToken)
        {
            return await _warehouseQueryRepository.GetAllWarehousesSelectAsync();
        }
    }
}
