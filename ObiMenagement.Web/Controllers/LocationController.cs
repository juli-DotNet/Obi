using Microsoft.AspNetCore.Mvc;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;
using ObiMenagement.Web.Models;

namespace ObiMenagement.Web.Controllers;

public class LocationController : Controller
{
    private readonly ILocationService _locationService;

    public LocationController(ILocationService locationService)
    {
        this._locationService = locationService;
    }

    #region Parse

    Location Parse(LocationViewModel model)
    {
        return new Location()
        {
            Id = model.Id,
            Name = model.Name,
            IsValid = true,
            KmFromMainCompany = model.KmFromMainCompany,
            Country = new Country()
            {
                Id = model.CountryId
            },
            City=new City { Id=model.CityId}

        };
    }
    LocationViewModel Parse(Location model)
    {
        return new LocationViewModel()
        {
            Id = model.Id,
            Name = model.Name,
            CountryId = model.Country?.Id ?? 0,
            Country = model.Country?.Name ?? "",
            City=model.City?.Name??"",
            CityId=model.City?.Id??0,
            KmFromMainCompany = model.KmFromMainCompany,
        };
    }


    #endregion
    public async Task<IActionResult> Index()
    {
        var result = await _locationService.GetAllAsync();
        if (!result.IsSuccessful)
        {
            ModelState.AddModelError("", result.Message);
        }
        return View(result.Result.Select(Parse));
    }

    public IActionResult Create()
    {
        var model = new LocationViewModel();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LocationViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.Id = 0;
            var result = await _locationService.CreateAsync(Parse(model));
            if (!result.IsSuccessful)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }
            return RedirectToAction("Index");
        }


        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var result = await _locationService.GetByIdAsync(id);

        if (!result.IsSuccessful)
        {
            ModelState.AddModelError("", result.Message);
            return View(new LocationViewModel());
        }

        return View(Parse(result.Result));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(LocationViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _locationService.EditAsync(Parse(model));
            if (!result.IsSuccessful)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);

            }
            return RedirectToAction("Index");
        }
        return View(model);

    }
    public async Task<IActionResult> Details(int id)
    {
        var response = await _locationService.GetByIdAsync(id);

        if (!response.IsSuccessful)
        {
            ModelState.AddModelError("", response.Message);
            return View(new LocationViewModel());
        }
        return View(Parse(response.Result));
    }
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _locationService.DeleteAsync(id);
        return Json(new GenericViewModel
        {
            IsSuccessful = response.IsSuccessful,
            ErrorMessage = response.Message
        });
    }
}