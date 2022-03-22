using ObiMenagement.Core.Models;

namespace ObiMenagement.Web.Models;

public class ExpenseTypeViewModel
{
    public ExpenseTypeViewModel()
    {
        Name = "";
        Currency = "";
        CurrencySymbol = "";
        PaymentType = "";
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int CurrencyId { get; set; }
    public string Currency { get; set; }
    public string CurrencySymbol { get; set; }
    public int PaymentTypeId { get; set; }
    public string PaymentType { get; set; }
    public bool IsFuel { get; set; }
    public bool IsPrepaymentGivenToEmployee { get; set; }
}