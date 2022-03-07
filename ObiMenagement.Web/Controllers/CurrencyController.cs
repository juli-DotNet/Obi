using Microsoft.AspNetCore.Mvc;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;
using ObiMenagement.Web.Models;

namespace ObiMenagement.Web.Controllers;

public class CurrencyController : Controller
{
    private readonly ICurrencyService _currencyService;

    public CurrencyController(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    #region Parse

    Currency Parse(CurrencyViewModel model)
    {
        return new Currency()
        {
            Id = model.Id,
            Name = model.Name,
            IsValid = true,
            IsDefault = model.IsDefault,
            Symbol = model.Symbol,
            Country = new Country()
            {
                Id = model.CountryId
            }
        };
    }
    CurrencyViewModel Parse(Currency model)
    {
        return new CurrencyViewModel()
        {
            Id = model.Id,
            Name = model.Name,
            CountryId = model.Country?.Id??0,
            Country = model.Country?.Name??"",
            Symbol = model.Symbol,
            IsDefault = model.IsDefault
        };
    }
    

    #endregion
    public async Task<IActionResult> Index()
    {
        var result = await _currencyService.GetAllAsync();
        if (!result.IsSuccessful)
        {
            ModelState.AddModelError("", result.Message);
        }
        return View(result.Result.Select(Parse));
    }

    public IActionResult Create()
    {
        var model = new CurrencyViewModel();
        return View(model);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CurrencyViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.Id = 0;
            var result = await _currencyService.CreateAsync(Parse(model));
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
        var result = await _currencyService.GetByIdAsync(id);
    
        if (!result.IsSuccessful)
        {
            ModelState.AddModelError("", result.Message);
            return View(new CurrencyViewModel());
        }
    
        return View(Parse(result.Result));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CurrencyViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _currencyService.EditAsync(Parse(model));
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
        var response = await _currencyService.GetByIdAsync(id);
    
        if (!response.IsSuccessful)
        {
            ModelState.AddModelError("", response.Message);
            return View(new CurrencyViewModel());
        }
        return View(Parse(response.Result));
    }
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _currencyService.DeleteAsync(id);
        return Json(new GenericViewModel
        {
            IsSuccessful = response.IsSuccessful,
            ErrorMessage = response.Message
        });
    }
}