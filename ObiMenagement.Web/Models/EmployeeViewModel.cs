﻿namespace ObiMenagement.Web.Models;

public class EmployeeViewModel
{
    public int Id { get; set; }
    public string Person { get; set; }
    public int PersonID { get; set; }
    public DateTime StartingDate { get; set; }
    public DateTime? EndingDate { get; set; }
    public string DefaultTruckBase { get; set; }
    public int DefaultTruckBaseId { get; set; }
    public string LeaveNote { get; set; } = "";
}