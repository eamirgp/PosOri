namespace Pos.Application.Features.Purchase.Queries.GetAllPurchases
{
    public record PurchaseDto(
        Guid Id,
        DateTime IssueDate,
        Guid WarehouseId,
        string Warehouse,
        Guid VoucherTypeId,
        string VoucherType,
        string Serie,
        string Number,
        Guid PersonId,
        string PersonDocumentNumber,
        string PersonName,
        Guid CurrencyId,
        string Currency,
        decimal Total
        );
}
