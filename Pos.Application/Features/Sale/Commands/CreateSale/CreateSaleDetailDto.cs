namespace Pos.Application.Features.Sale.Commands.CreateSale
{
    public record CreateSaleDetailDto(
        Guid ProductId,
        Guid UnitOfMeasureId,
        Guid IGVTypeId,
        decimal Quantity,
        decimal UnitPrice
        );
}
