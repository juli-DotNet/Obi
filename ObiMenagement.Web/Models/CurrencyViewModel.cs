namespace ObiMenagement.Web.Models;

public class CurrencyViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public int CountryId { get; set; }
    public string Country { get; set; }
}