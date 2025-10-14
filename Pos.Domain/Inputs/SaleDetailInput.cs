using Pos.Domain.Entities;

namespace Pos.Domain.Inputs
{
    public record SaleDetailInput(
        Guid ProductId,
        Guid UnitOfMeasureId,
        IGVType IGVType,
        decimal Quantity,
        decimal UnitPrice
        );
}
