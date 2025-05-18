namespace database_api.Dtos.GamingGear
{
    public class GamingGearDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public double Price { get; set; }
        public double PriceDiscount { get; set; }
        public string Category { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
    }
}
