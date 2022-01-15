namespace ObiMenagement.Core.Models;

public class RoadClient:IdLongBaseModel
{
    public Client Client { get; set; }
    public Location StartingLocation { get; set; }
    public Location DestinationLocation { get; set; }
    public decimal Price { get; set; }
    public Currency Currency { get; set; }
    public decimal Amount { get; set; }
    public decimal WeightInKg { get; set; }
    public decimal VolumeInM3 { get; set; }
    public decimal LDM { get; set; }
    public string Other { get; set; }
}