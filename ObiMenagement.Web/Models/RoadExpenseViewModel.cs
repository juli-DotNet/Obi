namespace ObiMenagement.Web.Models;

public class RoadExpenseViewModel
{
    public int Id { get; set; }
    public string ExpenseType { get; set; }
    public int ExpenseTypeId { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public int CurrencyId { get; set; }
    public string Currency { get; set; }
    public string CurrencySymbol { get; set; }
    public int PaymentTypeId { get; set; }
    public string PaymentType { get; set; }
    public string Name { get; set; }
    public string Note { get; set; }
    public int CountryId { get; set; }
    public string Country { get; set; }
}