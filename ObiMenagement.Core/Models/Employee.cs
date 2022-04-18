using System;

namespace ObiMenagement.Core.Models;

public class Employee : IdBaseModel
{
    public Person Person { get; set; }
    public DateTime StartingDate { get; set; }
    public DateTime? EndingDate { get; set; }
    public TruckBase DefaultTruckBase { get; set; }
    public TruckContainer DefaultTruckContainer { get; set; }
    public string LeaveNote { get; set; }
    public override string ToString()
    {
        return $"{Person.Name};{Person.LastName}";
    }
}