using System.ComponentModel.DataAnnotations;

namespace database_api.Dtos.Television
{
    public class CreateTelevisionDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public double Price { get; set; }

        public double PriceDiscount { get; set; }
    }
}
