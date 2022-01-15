namespace ObiMenagement.Core.Models;

public class ClientContact:IdBaseModel
{
    public Person Person { get; set; }
    public Client Client { get; set; }
}