namespace database_api.Models
{
    public class PaginatedResult<T> where T : class
    {
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public string? NextCursor { get; set; }
        public bool HasMore { get; set; }
    }
} 