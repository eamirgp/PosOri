using MediatR;

namespace Pos.Application.Features.Product.Commands.UpdateProduct
{
    public record UpdateProductRequest(
        Guid Id,
        Guid CategoryId,
        string Code,
        string Name,
        string? Description,
        decimal PurchasePrice,
        decimal SalePrice
        ) : IRequest<Unit>;
}
