using Microsoft.AspNetCore.Mvc;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;
using ObiMenagement.Web.Models;

namespace ObiMenagement.Web.Controllers;

public class JsonController : Controller
{
    private readonly ICountryService _countryService;
    private readonly ICityService _cityService;
    private readonly IPersonService _personService;
    private readonly ITrackBaseService _trackBaseService;
    private readonly ILocationService _locationService;

    public JsonController(ICountryService countryService,ICityService cityService,IPersonService personService,ITrackBaseService trackBaseService,ILocationService locationService)
    {
        _countryService = countryService;
        this._cityService = cityService;
        this._personService = personService;
        this._trackBaseService = trackBaseService;
        this._locationService = locationService;
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

    [HttpGet]
    public async Task<IActionResult> GetPersons(string search, int page)
    {

        var serviceResponse = await _personService.GetAllWithoutMetadataAsync();

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
    public async Task<IActionResult> GetTruckBases(string search, int page)
    {

        var serviceResponse = await _trackBaseService.GetAllAsync();

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
    public async Task<IActionResult> GetLocations(string search, int page)
    {

        var serviceResponse = await _locationService.GetAllAsync();

        var result = new JsonGenericModel();
        if (serviceResponse.IsSuccessful)
        {
            result.IsSuccessful = true;
            result.Items = serviceResponse.Result.Select(a => Parse(a)).Where(a=>a.Text.Contains(search)).ToList();
        }
        else
        {
            result.ErrorMessage = serviceResponse.Message;
        }

        return Json(result);
    }

    private SelectDataDTO Parse(Location model)
    {
        return new SelectDataDTO()
        {
            Id = model.Id.ToString(),
            Text =$"{model.Country.Name}:{model.City.Name}:{model.Name}"
        };
    }
    private SelectDataDTO Parse(Person model)
    {
        return new SelectDataDTO()
        {
            Id = model.Id.ToString(),
            Text = model.Name
        };
    }
    private SelectDataDTO Parse(TruckBase model)
    {
        return new SelectDataDTO()
        {
            Id = model.Id.ToString(),
            Text = model.Plate
        };
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