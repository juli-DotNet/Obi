using System.Security.Principal;

namespace ObiMenagement.Core.Models;

public class City:IdBaseModel
{
    public string Name { get; set; }
    public Country Country { get; set; }
}