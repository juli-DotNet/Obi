namespace ObiMenagement.Core.Models;

public class RoadExpense:IdLongBaseModel
{
    public ExpenseType ExpenseType { get; set; }
    public decimal Quantity { get; set; }
    public Payment Payment { get; set; }
    public string Location { get; set; }
    public Country Country { get; set; }
}

public class Payment
{
    public decimal Price { get; set; }
    public Currency Currency { get; set; }
    public PaymentTypeEnum PaymentTypeEnum { get; set; }
}