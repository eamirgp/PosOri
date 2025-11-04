using MediatR;
using Pos.Application.Shared.Pagination;

namespace Pos.Application.Features.Sale.Queries.GetAllSales
{
    public record GetAllSalesRequest(
        PaginationParams param,
        Guid? warehouseId
        ) : IRequest<PaginatedResult<SaleDto>>;
}
