namespace Pos.App.Shared.Pagination
{
    public record PaginatedResultModel<T>(
        List<T> Items,
        int PageNumber,
        int PageSize,
        int TotalCount,
        int TotalPage,
        bool HasPreviousPage,
        bool HasNextPage
        );
}
