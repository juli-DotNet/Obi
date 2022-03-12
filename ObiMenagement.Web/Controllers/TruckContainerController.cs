using Microsoft.AspNetCore.Mvc;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;
using ObiMenagement.Web.Models;

namespace ObiMenagement.Web.Controllers;

public class TruckContainerController : Controller
{ 
    private readonly ITrackContainerService _trackContainerService;

    public TruckContainerController(ITrackContainerService trackContainerService)
    {
        this._trackContainerService = trackContainerService;
    }

    #region Parse

    TruckContainer Parse(TruckContainerViewModel model)
    {
        return new TruckContainer()
        {
            Id = model.Id,
            Color=model.Color,
            IsValid=true,
            Description=model.Description,
            Plate=model.Plate

        };
    }
    TruckContainerViewModel Parse(TruckContainer model)
    {
        return new TruckContainerViewModel()
        {
            Id = model.Id,
            Color = model.Color,
            Description = model.Description,
            Plate = model.Plate
        };
    }


    #endregion
    public async Task<IActionResult> Index()
    {
        var result = await _trackContainerService.GetAllAsync();
        if (!result.IsSuccessful)
        {
            ModelState.AddModelError("", result.Message);
        }
        return View(result.Result.Select(Parse));
    }

    public IActionResult Create()
    {
        var model = new TruckContainerViewModel();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TruckContainerViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.Id = 0;
            var result = await _trackContainerService.CreateAsync(Parse(model));
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
        var result = await _trackContainerService.GetByIdAsync(id);

        if (!result.IsSuccessful)
        {
            ModelState.AddModelError("", result.Message);
            return View(new TruckContainerViewModel());
        }

        return View(Parse(result.Result));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(TruckContainerViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _trackContainerService.EditAsync(Parse(model));
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
        var response = await _trackContainerService.GetByIdAsync(id);

        if (!response.IsSuccessful)
        {
            ModelState.AddModelError("", response.Message);
            return View(new TruckContainerViewModel());
        }
        return View(Parse(response.Result));
    }
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _trackContainerService.DeleteAsync(id);
        return Json(new GenericViewModel
        {
            IsSuccessful = response.IsSuccessful,
            ErrorMessage = response.Message
        });
    }
}
