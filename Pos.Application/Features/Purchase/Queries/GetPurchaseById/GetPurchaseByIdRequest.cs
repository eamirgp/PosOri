using MediatR;
using Pos.Application.Shared.Result;

namespace Pos.Application.Features.Purchase.Queries.GetPurchaseById
{
    public record GetPurchaseByIdRequest(Guid Id) : IRequest<Result<PurchaseCompleteDto>>;
}
