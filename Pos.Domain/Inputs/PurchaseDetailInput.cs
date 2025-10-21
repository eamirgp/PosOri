using Pos.Domain.Entities;

namespace Pos.Domain.Inputs
{
    public record PurchaseDetailInput(
        Product Product,
        IGVType IGVType,
        decimal Quantity,
        decimal UnitValue
        );
}
