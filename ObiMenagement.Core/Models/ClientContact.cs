namespace ObiMenagement.Core.Models;

public class ClientContact:BaseModel
{
    public Person Person { get; set; }
    public Client Client { get; set; }
}