namespace Pos.Application.Features.Sale.Commands.CreateSale
{
    public record CreateSaleDetailDto(
        Guid ProductId,
        Guid IGVTypeId,
        decimal Quantity,
        decimal UnitPrice
        );
}
