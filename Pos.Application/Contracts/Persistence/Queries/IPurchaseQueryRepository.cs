using Pos.Application.Features.Purchase.Queries.GetAllPurchases;
using Pos.Application.Features.Purchase.Queries.GetPurchaseById;
using Pos.Application.Shared.Pagination;

namespace Pos.Application.Contracts.Persistence.Queries
{
    public interface IPurchaseQueryRepository
    {
        Task<PaginatedResult<PurchaseDto>> GetAllPurchasesAsync(PaginationParams param, Guid? warehouseId);
        Task<PurchaseCompleteDto?> GetPurchaseByIdAsync(Guid purchaseId);
    }
}
