using Pos.Domain.Entities;

namespace Pos.Domain.Inputs
{
    public record SaleDetailInput(
        Product Product,
        IGVType IGVType,
        decimal Quantity,
        decimal UnitPrice
        );
}
