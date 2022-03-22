namespace ObiMenagement.Web.Models;

public class TableViewModel<T> : GenericViewModel
{
    public List<T> Items { get; set; }
}