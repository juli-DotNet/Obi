using System.Collections.Generic;

namespace ObiMenagement.Core.Models;

public class Client : IdBaseModel
{
    public string Name { get; set; }
    public string Notes { get; set; }
    public virtual  List<ClientContact> Contacts { get; set; }
    public virtual Location Location { get; set; }
}