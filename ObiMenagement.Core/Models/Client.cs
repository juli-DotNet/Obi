namespace ObiMenagement.Core.Models;

public class Client : BaseModel
{
    public string Name { get; set; }
    public string Notes { get; set; }
    public virtual  List<ClientContact> Contacts { get; set; }
    public Location Location { get; set; }
}