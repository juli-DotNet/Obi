namespace ObiMenagement.Core.Models;

public class RoadData
{
    public long Id { get; set; }
    public bool IsValid { get; set; }
    public TruckBase TruckBase { get; set; }
    public TruckContainer TruckContainer { get; set; }
    public DateTime StartingDate { get; set; }
    public DateTime EndingDate { get; set; }
    public Location StartingLocation { get; set; }
    public Location DestinationLocation { get; set; }
    public long TotalKM { get; set; }
    public decimal Price { get; set; } //calculated
    public List<RoadClient> Clients { get; set; }
    public List<RoadExpense> RoadFuels { get; set; }
    public bool IsExport { get; set; }
}