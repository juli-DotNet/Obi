namespace ObiMenagement.Core.Models;

public class Location : IdBaseModel
{
    public string Name { get; set; }
    public decimal KmFromMainCompany { get; set; }
    public County County { get; set; }
    public City City { get; set; }
}