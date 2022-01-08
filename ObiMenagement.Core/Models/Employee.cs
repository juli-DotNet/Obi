using System;

namespace ObiMenagement.Core.Models;

public class Employee : BaseModel
{
    public Person Person { get; set; }
    public DateTime StartingDate { get; set; }
    public DateTime? EndingDate { get; set; }
    public TruckBase DefaultTruckBase { get; set; }
    public string LeaveNote { get; set; }
}