namespace ObiMenagement.Web.Models;

public class ClientViewModel
{
    public int Id { get; set; }
    public string Name { get;  set; }
    public string Notes { get;  set; }
    public string Contacts { get;  set; }
    public string Location { get;  set; }
    public int LocationId { get; set; }
    public List<int> Contancts { get; set; }
}
