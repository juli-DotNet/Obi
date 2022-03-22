namespace ObiMenagement.Core.Models;

public class Payment:ValueObject
{
    public decimal Price { get; set; }
    public Currency Currency { get; set; }
    public PaymentTypeEnum PaymentTypeEnum { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Price;
        yield return Currency.Name;
        yield return PaymentTypeEnum;
    }
}