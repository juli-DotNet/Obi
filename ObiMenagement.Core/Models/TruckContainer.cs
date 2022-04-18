namespace ObiMenagement.Core.Models;

public class TruckContainer:IdBaseModel
{
    public string Plate { get; set; }
    public string Color { get; set; }
    public string Description { get; set; }

    public override string ToString()
    {
        return $"{Plate}";
    }
}