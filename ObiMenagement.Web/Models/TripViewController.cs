namespace ObiMenagement.Web.Models;

public class TripViewModel
{
    public long Id { get; set; }
    public int Number { get; set; }
    public string TruckBase { get; set; }
    public int TruckBaseId { get; set; }
    public string TruckContainer { get; set; }
    public int TruckContainerId { get; set; }
    public string Employee { get; set; }
    public int EmployeeId { get; set; }
    public long StartingTrucKm { get; set; }
    public long EndingTrucKm { get; set; }
    public long TotalKM { get; set; }
    public decimal StartingAmountOfFuel { get; set; }
    public decimal EndingAmountOfFuel { get; set; }
    
    //public List<RoadData> Roads { get; set; }
    //public List<RoadExpense> Expenses { get; set; }
    //public decimal TotalWage { get; set; }
}