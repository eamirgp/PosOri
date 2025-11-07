using MediatR;
using Pos.Application.Contracts.Persistence.Queries;
using Pos.Application.Shared.Result;

namespace Pos.Application.Features.Purchase.Queries.GetPurchaseById
{
    public class GetPurchaseByIdRequestHandler : IRequestHandler<GetPurchaseByIdRequest, Result<PurchaseCompleteDto>>
    {
        private readonly IPurchaseQueryRepository _purchaseQueryRepository;

        public GetPurchaseByIdRequestHandler(IPurchaseQueryRepository purchaseQueryRepository)
        {
            _purchaseQueryRepository = purchaseQueryRepository;
        }

        public async Task<Result<PurchaseCompleteDto>> Handle(GetPurchaseByIdRequest request, CancellationToken cancellationToken)
        {
            var purchase = await _purchaseQueryRepository.GetPurchaseByIdAsync(request.Id);

            if (purchase is null)
                return Result<PurchaseCompleteDto>.Failure(new List<string> { $"La compra con ID '{request.Id}' no existe." }, 404);

            return Result<PurchaseCompleteDto>.Success(purchase);
        }
    }
}
