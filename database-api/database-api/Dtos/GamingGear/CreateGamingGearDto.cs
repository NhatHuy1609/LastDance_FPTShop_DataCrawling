using System.ComponentModel.DataAnnotations;

namespace database_api.Dtos.GamingGear
{
    public class CreateGamingGearDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Url { get; set; } = string.Empty;

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        [Range(0, double.MaxValue)]
        public double PriceDiscount { get; set; }

        [Required]
        [MaxLength(50)]
        public string Category { get; set; } = string.Empty;

        public bool IsAvailable { get; set; } = true;
    }
}
