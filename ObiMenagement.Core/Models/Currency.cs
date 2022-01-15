namespace ObiMenagement.Core.Models;

public class Currency : IdBaseModel
{
    public string Name { get; set; }
    public string Symbol { get; set; }
    public bool IsDefault { get; set; }
    public County County { get; set; }
}