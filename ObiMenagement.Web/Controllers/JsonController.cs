using Microsoft.AspNetCore.Mvc;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;
using ObiMenagement.Web.Models;

namespace ObiMenagement.Web.Controllers;

public class JsonController : Controller
{
    private readonly ICountryService _countryService;
    private readonly ICityService _cityService;

    public JsonController(ICountryService countryService,ICityService cityService)
    {
        _countryService = countryService;
        this._cityService = cityService;
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
    [HttpGet]
    public async Task<IActionResult> GetCities(string search, int page,int countryId)
    {

        var serviceResponse = await _cityService.GetAllWithoutMetadataAsync(countryId);

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

    private SelectDataDTO Parse(City model)
    {
        return new SelectDataDTO()
        {
            Id = model.Id.ToString(),
            Text = model.Name
        };
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