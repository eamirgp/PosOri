namespace Pos.Application.Features.Sale.Queries.GetSaleById
{
    public record SaleCompleteDto(
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
        decimal TaxAmount,
        decimal Total,

        List<SaleDetailItemDto> Details
        );

    public record SaleDetailItemDto(
        Guid Id,
        Guid ProductId,
        string ProductCode,
        string ProductName,
        Guid UnitOfMeasureId,
        string UnitOfMeasure,
        Guid IGVTypeId,
        string IGVType,
        decimal Quantity,
        decimal UnitPrice,
        decimal Amount,
        decimal TaxAmount,
        decimal LineTotal
        );
}
