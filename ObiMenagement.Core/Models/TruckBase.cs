namespace ObiMenagement.Core.Models;

public class TruckBase : IdBaseModel
{
    public string Plate { get; set; }
    public string Color { get; set; }
    public string Description { get; set; }
}