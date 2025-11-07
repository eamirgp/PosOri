using Pos.Application.Features.Sale.Queries.GetAllSales;
using Pos.Application.Features.Sale.Queries.GetSaleById;
using Pos.Application.Shared.Pagination;

namespace Pos.Application.Contracts.Persistence.Queries
{
    public interface ISaleQueryRepository
    {
        Task<PaginatedResult<SaleDto>> GetAllSalesAsync(PaginationParams param, Guid? warehouseId);
        Task<SaleCompleteDto?> GetSaleByIdAsync(Guid saleId);
    }
}
