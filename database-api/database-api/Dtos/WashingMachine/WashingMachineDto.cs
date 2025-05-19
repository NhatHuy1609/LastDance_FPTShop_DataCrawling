namespace database_api.Dtos.WashingMachine;

public class WashingMachineDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string ImageUrl { get; set; }
    public double Price { get; set; }
    public double PriceDiscount { get; set; }
    public string Category { get; set; }
    public string isAvailable { get; set; }
}