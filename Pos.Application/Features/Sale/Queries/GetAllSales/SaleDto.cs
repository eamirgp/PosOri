namespace Pos.Application.Features.Sale.Queries.GetAllSales
{
    public record SaleDto(
        Guid Id,
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
