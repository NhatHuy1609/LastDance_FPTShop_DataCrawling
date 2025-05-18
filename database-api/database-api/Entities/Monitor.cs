namespace database_api.Entities
{
    public class Monitor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public double? Price { get; set; }
        public double? PriceDiscount { get; set; }
        public string? Category { get; set; }
        public string? isAvailable { get; set; }
    }
}