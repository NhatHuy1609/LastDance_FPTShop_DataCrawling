using System.ComponentModel.DataAnnotations;

namespace database_api.Dtos.WashingMachine;

public class CreateWashingMachineDto
{
    [Required]
    public string Name { get; set; }
        
    [Required]
    public string Url { get; set; }
        
    [Required]
    public string ImageUrl { get; set; }
        
    [Required]
    [Range(0, double.MaxValue)]
    public double Price { get; set; }
        
    [Range(0, double.MaxValue)]
    public double PriceDiscount { get; set; }
        
    [Required]
    public string Category { get; set; }
        
    public bool? IsAvailable { get; set; }
}