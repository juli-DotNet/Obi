namespace ObiMenagement.Core.Models;

public class Payment:ValueObject
{
    public Currency Currency { get; set; }
    public PaymentTypeEnum PaymentTypeEnum { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Currency.Name;
        yield return PaymentTypeEnum;
    }
}