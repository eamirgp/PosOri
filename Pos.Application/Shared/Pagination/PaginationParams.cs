namespace Pos.Application.Shared.Pagination
{
    public class PaginationParams
    {
        private int _pageNumber = 1;
        private int _pageSize = 10;

        private const int MinPageSize = 5;
        private const int MaxPageSize = 10;

        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = value < 1 ? 1 : value;
        }
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value < MinPageSize ? MinPageSize : value > MaxPageSize ? MaxPageSize : value;
        }
        
        public string? SearchTerm { get; set; }
    }
}
