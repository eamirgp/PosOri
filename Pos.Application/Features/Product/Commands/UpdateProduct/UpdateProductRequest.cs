using MediatR;
using Pos.Application.Shared.Result;

namespace Pos.Application.Features.Product.Commands.UpdateProduct
{
    public record UpdateProductRequest(
        Guid Id,
        Guid UnitOfMeasureId,
        Guid IGVTypeId,
        Guid CategoryId,
        string Code,
        string Name,
        string? Description,
        decimal PurchasePrice,
        decimal SalePrice
        ) : IRequest<Result<Unit>>;
}
