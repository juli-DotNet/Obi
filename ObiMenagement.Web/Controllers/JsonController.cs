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
    private readonly ICurrencyService _currencyService;
    private readonly IEmployeeService _employeeService;
    private readonly ITrackContainerService _trackContainerService;
    private readonly IExpenseTypeService _expenseTypeService;

    public JsonController(ICountryService countryService, ICityService cityService, IPersonService personService, 
        ITrackBaseService trackBaseService, ILocationService locationService,ICurrencyService currencyService,
        IEmployeeService employeeService,ITrackContainerService trackContainerService,IExpenseTypeService expenseTypeService)
    {
        _countryService = countryService;
        _cityService = cityService;
        _personService = personService;
        _trackBaseService = trackBaseService;
        _locationService = locationService;
        _currencyService = currencyService;
        _employeeService = employeeService;
        _trackContainerService = trackContainerService;
        _expenseTypeService = expenseTypeService;
    }
    [HttpGet]
    public async Task<IActionResult> GetCountries(string search, int page)
    {
        search ??= "";
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
    public async Task<IActionResult> GetCities(string search, int page, int countryId)
    {
        search ??= "";
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
        search ??= "";

        var serviceResponse = await _personService.GetAllWithoutMetadataAsync();

        var result = new JsonGenericModel();
        if (serviceResponse.IsSuccessful)
        {
            result.IsSuccessful = true;
            result.Items = serviceResponse.Result.Select(a => Parse(a)).Where(a => a.Text.Contains(search));
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
        search ??= "";
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
    public async Task<IActionResult> GetTruckContainers(string search, int page)
    {
        search ??= "";
        var serviceResponse = await _trackContainerService.GetAllAsync();

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
        search ??= "";
        var serviceResponse = await _locationService.GetAllAsync();

        var result = new JsonGenericModel();
        if (serviceResponse.IsSuccessful)
        {
            result.IsSuccessful = true;
            result.Items = serviceResponse.Result.Select(a => Parse(a)).Where(a => a.Text.Contains(search)).ToList();
        }
        else
        {
            result.ErrorMessage = serviceResponse.Message;
        }

        return Json(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetPaymentTypes(string search, int page)
    {
        search ??= "";

        var allTypes = Enum.GetValues(typeof(PaymentTypeEnum)).Cast<PaymentTypeEnum>().Select(a => new SelectDataDTO
        {

            Id = ((int)a).ToString(),
            Text = a.ToString()
        }).ToList();

        var result = new JsonGenericModel() { IsSuccessful = true };
        result.Items = allTypes.Where(a => a.Text.Contains(search));
        return Json(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetCurrencies(string search, int page)
    {
        search ??= "";
        var serviceResponse = await _currencyService.GetAllAsync();

        var result = new JsonGenericModel();
        if (serviceResponse.IsSuccessful)
        {
            result.IsSuccessful = true;
            result.Items = serviceResponse.Result.Select(a => Parse(a)).Where(a => a.Text.Contains(search)).ToList();
        }
        else
        {
            result.ErrorMessage = serviceResponse.Message;
        }

        return Json(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetEmployees(string search, int page)
    {
        search ??= "";
        var serviceResponse = await _employeeService.GetAllAsync();

        var result = new JsonGenericModel();
        if (serviceResponse.IsSuccessful)
        {
            result.IsSuccessful = true;
            result.Items = serviceResponse.Result.Select(a => Parse(a)).Where(a => a.Text.Contains(search)).ToList();
        }
        else
        {
            result.ErrorMessage = serviceResponse.Message;
        }

        return Json(result);
    }


    [HttpGet]
    public async Task<IActionResult> GetExpenseTypes(string search, int page)
    {
        search ??= "";
        var serviceResponse = await _expenseTypeService.GetAllAsync();

        var result = new JsonGenericModel();
        if (serviceResponse.IsSuccessful)
        {
            result.IsSuccessful = true;
            result.Items = serviceResponse.Result.Select(a => Parse(a)).Where(a => a.Text.Contains(search)).ToList();
        }
        else
        {
            result.ErrorMessage = serviceResponse.Message;
        }

        return Json(result);
    }
    private SelectDataDTO Parse(ExpenseType model)
    {
        return new SelectDataDTO()
        {
            Id = model.Id.ToString(),
            Text = model.Name.ToString()
        };
    }
    private SelectDataDTO Parse(TruckContainer model)
    {
        return new SelectDataDTO()
        {
            Id = model.Id.ToString(),
            Text = model.Plate.ToString()
        };
    }
    private SelectDataDTO Parse(Employee model)
    {
        return new SelectDataDTO()
        {
            Id = model.Id.ToString(),
            Text = model.ToString()
        };
    }
    private SelectDataDTO Parse(Currency model)
    {
        return new SelectDataDTO()
        {
            Id = model.Id.ToString(),
            Text = model.Name
        };
    }
    private SelectDataDTO Parse(Location model)
    {
        return new SelectDataDTO()
        {
            Id = model.Id.ToString(),
            Text = model.ToString()
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