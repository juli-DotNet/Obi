using System.Security.Principal;

namespace ObiMenagement.Core.Models;

public class City:BaseModel
{
    public string Name { get; set; }
    public County County { get; set; }
}