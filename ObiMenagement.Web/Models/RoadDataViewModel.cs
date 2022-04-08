namespace ObiMenagement.Web.Models;

public class RoadDataViewModel
{
    public long? Id { get; set; }
    public string StartingDate { get; set; }
    public string? EndingDate { get; set; }
    public long? TotalKM { get; set; }
    public decimal Price { get; set; }
    public bool IsExport { get; set; }
    public int StartingLocationId { get; set; }
    public string? StartingLocation { get; set; }
    public int DestinationLocationId { get; set; }
    public string? DestinationLocation { get; set; }
    public long TripId { get; set; }
    public string Trip { get; set; }
    public int TruckBaseId { get; set; }
    public string? TruckBase { get; set; }
    public int TruckContainerId { get; set; }
    public string? TruckContainer { get; set; }
    //public virtual List<RoadClient> Clients { get; set; }
    //public virtual List<RoadExpense> RoadFuels { get; set; }

}