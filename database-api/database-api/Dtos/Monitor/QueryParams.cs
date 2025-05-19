namespace database_api.Dtos.Monitor
{
    /// <summary>
    /// Query parameters for monitoring
    /// Sample: /api/monitors?pageNumber=1&pageSize=10&searchTerm=man%20hinh&minPrice=1000000&maxPrice=2000000&minDiscountedPrice=10&maxDiscountedPrice=20&isDescending=false&sortBy=name
    /// </summary>
    public class QueryParams
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
        public string? Category { get; set; }
        public string? SortBy { get; set; } = "Id";
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public bool IsDescending { get; set; } = false;
    }
}