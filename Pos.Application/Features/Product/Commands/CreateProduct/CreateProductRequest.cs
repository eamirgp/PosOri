using MediatR;
using Pos.Application.Shared.Result;

namespace Pos.Application.Features.Product.Commands.CreateProduct
{
    public record CreateProductRequest(
        Guid UnitOfMeasureId,
        Guid CategoryId,
        string Code,
        string Name,
        string? Description,
        decimal PurchasePrice,
        decimal SalePrice
        ) : IRequest<Result<Guid>>;
}
