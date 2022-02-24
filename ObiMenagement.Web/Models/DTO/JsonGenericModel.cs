namespace ObiMenagement.Web.Models;

public class JsonGenericModel : GenericViewModel
{
    public IEnumerable<SelectDataDTO> Items { get; set; }
}