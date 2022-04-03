namespace ObiMenagement.Web.Models;

public class DataViewModel<T> : GenericViewModel
{
    public T Data { get; set; }
}
