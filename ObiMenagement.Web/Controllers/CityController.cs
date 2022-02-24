using Microsoft.AspNetCore.Mvc;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;
using ObiMenagement.Web.Models;

namespace ObiMenagement.Web.Controllers;

public class CityController : Controller
{
    private readonly ICityService _cityService;

    public CityController(ICityService cityService)
    {
        _cityService = cityService;
    }

    City Parse(CityViewModel model)
    {
        return new City
        {
            Id = model.Id,
            Name = model.Name,
            IsValid = true,
            Country = new Country()
            {
                Id = model.CountryId
            }
        };
    }
    CityViewModel Parse(City model)
    {
        return new CityViewModel()
        {
            Id = model.Id,
            Name = model.Name,
            CountryId = model.Country.Id,
            CountryName = model.Country.Name
        };
    }

    public async Task<IActionResult> Index()
    {
        var result = await _cityService.GetAllAsync();
        if (!result.IsSuccessful)
        {
            ModelState.AddModelError("", result.Message);
        }
        return View(result.Result.Select(Parse));
    }

    public IActionResult Create()
    {
        var model = new CityViewModel();
        return View(model);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CityViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.Id = 0;
            var result = await _cityService.CreateAsync(Parse(model));
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
        var result = await _cityService.GetByIdAsync(id);
    
        if (!result.IsSuccessful)
        {
            ModelState.AddModelError("", result.Message);
            return View(new CityViewModel());
        }
    
        return View(Parse(result.Result));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CityViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _cityService.EditAsync(Parse(model));
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
        var response = await _cityService.GetByIdAsync(id);
    
        if (!response.IsSuccessful)
        {
            ModelState.AddModelError("", response.Message);
            return View(new CityViewModel());
        }
        return View(Parse(response.Result));
    }
    
    
    
    public async Task<IActionResult> Delete(int id)
    {
    
        var response = await _cityService.DeleteAsync(id);
        return Json(new GenericViewModel
        {
            IsSuccessful = response.IsSuccessful,
            ErrorMessage = response.Message
        });
    }
}