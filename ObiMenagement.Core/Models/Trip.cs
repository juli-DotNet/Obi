using System.Collections.Generic;

namespace ObiMenagement.Core.Models;

public class Trip
{
    public long Id { get; set; }
    public bool IsValid { get; set; }
    public int Number { get; set; }
    public TruckBase TruckBase { get; set; }
    public TruckContainer TruckContainer { get; set; }
    public Employee Employee { get; set; }
    public List<RoadData> Roads { get; set; }
    public long StartingTrucKm { get; set; }
    public long EndingTrucKm { get; set; }
    public long TotalKM { get; set; }
    public int StartingAmountOfFuel { get; set; }
    public int EndingAmountOfFuel { get; set; }
    public List<RoadExpense> Expenses { get; set; }
    public decimal TotalWage { get; set; }
}