namespace ObiMenagement.Core.Models;

public class ExpenseType:IdBaseModel
{
    public string Name { get; set; }
    public Payment DefaultPayment { get; set; }
    public decimal Price { get; set; }
    public bool IsFuel { get; set; }
    public bool IsPrepaymentGivenToEmployees { get; set; }
    public override string ToString()
    {
        return Name;
    }

}