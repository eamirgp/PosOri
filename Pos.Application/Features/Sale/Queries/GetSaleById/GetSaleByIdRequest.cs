using MediatR;
using Pos.Application.Shared.Result;

namespace Pos.Application.Features.Sale.Queries.GetSaleById
{
    public record GetSaleByIdRequest(
        Guid Id
        ) : IRequest<Result<SaleCompleteDto>>;
}
