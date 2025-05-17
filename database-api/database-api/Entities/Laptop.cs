namespace database_api.Entities
{
    public class Laptop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public double PriceDiscount { get; set; }
    }
}
