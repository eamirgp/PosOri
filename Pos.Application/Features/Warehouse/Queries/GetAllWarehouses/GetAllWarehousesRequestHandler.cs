using MediatR;
using Pos.Application.Contracts.Queries;

namespace Pos.Application.Features.Warehouse.Queries.GetAllWarehouses
{
    public class GetAllWarehousesRequestHandler : IRequestHandler<GetAllWarehousesRequest, List<WarehouseDto>>
    {
        private readonly IWarehouseQueryRepository _warehouseQueryRepository;

        public GetAllWarehousesRequestHandler(IWarehouseQueryRepository warehouseQueryRepository)
        {
            _warehouseQueryRepository = warehouseQueryRepository;
        }

        public async Task<List<WarehouseDto>> Handle(GetAllWarehousesRequest request, CancellationToken cancellationToken)
        {
            return await _warehouseQueryRepository.GetAllWarehousesAsync();
        }
    }
}
