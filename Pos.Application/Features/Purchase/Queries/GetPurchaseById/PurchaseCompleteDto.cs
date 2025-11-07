namespace Pos.Application.Features.Purchase.Queries.GetPurchaseById
{
    public record PurchaseCompleteDto(
        Guid Id,
        DateTime IssueDate,
        DateTime CreatedDate,

        Guid WarehouseId,
        string Warehouse,

        Guid VoucherTypeId,
        string VoucherType,
        string Serie,
        string Number,

        Guid PersonId,
        string PersonDocumentType,
        string PersonDocumentNumber,
        string PersonName,

        Guid CurrencyId,
        string Currency,

        decimal SubTotal,
        decimal Exempt,
        decimal TaxAmount,
        decimal Total,

        List<PurchaseDetailItemDto> Details
        );

    public record PurchaseDetailItemDto(
        Guid Id,
        Guid ProductId,
        string ProductCode,
        string ProductName,
        Guid UnitOfMeasureId,
        string UnitOfMeasure,
        Guid IGVTypeId,
        string IGVType,
        decimal Quantity,
        decimal UnitValue,
        decimal Amount,
        decimal TaxAmount,
        decimal LineTotal
        );
}
