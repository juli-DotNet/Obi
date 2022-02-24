namespace ObiMenagement.Core.Models;

public class ExpenseType:IdBaseModel
{
    public string Name { get; set; }
    public Payment DefaultPayment { get; set; }
    public bool IsFuel { get; set; }
    public bool IsPositive { get; set; }//TODO: rename this
    
}