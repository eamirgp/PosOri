using MediatR;

namespace Pos.Application.Features.Product.Commands.CreateProduct
{
    public record CreateProductRequest(
        Guid CategoryId,
        string Code,
        string Name,
        string? Description,
        decimal PurchasePrice,
        decimal SalePrice
        ) : IRequest<Guid>;
}
