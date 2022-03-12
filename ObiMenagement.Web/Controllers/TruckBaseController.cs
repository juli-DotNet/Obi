using Microsoft.AspNetCore.Mvc;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;
using ObiMenagement.Web.Models;

namespace ObiMenagement.Web.Controllers;

public class TruckBaseController : Controller
{
    private readonly ITrackBaseService _trackBaseService;

    public TruckBaseController(ITrackBaseService trackBaseService)
    {
        this._trackBaseService = trackBaseService;
    }

    #region Parse

    TruckBase Parse(TrackBaseViewModel model)
    {
        return new TruckBase()
        {
            Id = model.Id,
            Plate = model.Plate,
            Color = model.Color,
            Description = model.Description,
            IsValid = true,

        };
    }
    TrackBaseViewModel Parse(TruckBase model)
    {
        return new TrackBaseViewModel()
        {
            Id = model.Id,
            Plate = model.Plate,
            Color = model.Color,
            Description = model.Description
        };
    }


    #endregion
    public async Task<IActionResult> Index()
    {
        var result = await _trackBaseService.GetAllAsync();
        if (!result.IsSuccessful)
        {
            ModelState.AddModelError("", result.Message);
        }
        return View(result.Result.Select(Parse));
    }

    public IActionResult Create()
    {
        var model = new TrackBaseViewModel();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TrackBaseViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.Id = 0;
            var result = await _trackBaseService.CreateAsync(Parse(model));
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
        var result = await _trackBaseService.GetByIdAsync(id);

        if (!result.IsSuccessful)
        {
            ModelState.AddModelError("", result.Message);
            return View(new TrackBaseViewModel());
        }

        return View(Parse(result.Result));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(TrackBaseViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _trackBaseService.EditAsync(Parse(model));
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
        var response = await _trackBaseService.GetByIdAsync(id);

        if (!response.IsSuccessful)
        {
            ModelState.AddModelError("", response.Message);
            return View(new TrackBaseViewModel());
        }
        return View(Parse(response.Result));
    }
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _trackBaseService.DeleteAsync(id);
        return Json(new GenericViewModel
        {
            IsSuccessful = response.IsSuccessful,
            ErrorMessage = response.Message
        });
    }
}