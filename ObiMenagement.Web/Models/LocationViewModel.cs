namespace ObiMenagement.Web.Models;

public class LocationViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal KmFromMainCompany { get; set; }
    public int CountryId { get; set; }
    public string Country { get; set; }

    public int CityId { get; set; }
    public string City { get; set; }
}