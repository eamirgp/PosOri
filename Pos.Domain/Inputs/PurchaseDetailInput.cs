using Pos.Domain.Entities;

namespace Pos.Domain.Inputs
{
    public record PurchaseDetailInput(Guid ProductId, Guid UnitOfMeasureId, IGVType IGVType, decimal Quantity, decimal UnitValue);
}
