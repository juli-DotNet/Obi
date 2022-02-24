using Microsoft.AspNetCore.Mvc;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;
using ObiMenagement.Web.Models;

namespace ObiMenagement.Web.Controllers;

public class JsonController : Controller
{
    private readonly ICountryService _countryService;

    public JsonController(ICountryService countryService)
    {
        _countryService = countryService;
    }
    [HttpGet]
    public async Task<IActionResult> GetCountries(string search, int page)
    {

        var serviceResponse = await _countryService.GetAllWithoutMetadataAsync();

        var result = new JsonGenericModel();
        if (serviceResponse.IsSuccessful)
        {
            result.IsSuccessful = true;
            result.Items = serviceResponse.Result.Select(a => Parse(a));
        }
        else
        {
            result.ErrorMessage = serviceResponse.Message;
        }

        return Json(result);
    }

    private SelectDataDTO Parse(Country country)
    {
        return new SelectDataDTO()
        {
            Id = country.Id.ToString(),
            Text = country.Name
        };
    }
}