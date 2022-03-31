using Microsoft.AspNetCore.Mvc;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;
using ObiMenagement.Web.Models;

namespace ObiMenagement.Web.Controllers;

public class TripController : Controller
{
    private readonly ITripService _tripService;

    public TripController(ITripService tripService)
    {
        _tripService = tripService;
    }

    #region Trip CURD

    public async Task<IActionResult> Index()
    {
        var result = await _tripService.GetAllAsync();
        if (!result.IsSuccessful)
        {
            ModelState.AddModelError("", result.Message);
        }

        return View(result.Result.Select(Parse));
    }

    public IActionResult Create()
    {
        var model = new TripViewModel();
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var result = await _tripService.GetByIdAsync(id);

        if (!result.IsSuccessful)
        {
            ModelState.AddModelError("", result.Message);
            return View(new TripViewModel());
        }

        return View(Parse(result.Result));
    }

    public async Task<IActionResult> Details(int id)
    {
        var response = await _tripService.GetByIdAsync(id);

        if (!response.IsSuccessful)
        {
            ModelState.AddModelError("", response.Message);
            return View(new TripViewModel());
        }

        return View(Parse(response.Result));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var response = await _tripService.DeleteAsync(id);
        return Json(new GenericViewModel
        {
            IsSuccessful = response.IsSuccessful,
            ErrorMessage = response.Message
        });
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TripViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.Id = 0;
            var result = await _tripService.CreateAsync(Parse(model));
            if (!result.IsSuccessful)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        return View(model);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(TripViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _tripService.EditAsync(Parse(model));
            if (!result.IsSuccessful)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        return View(model);
    }

    #endregion

    #region Parse

    Trip Parse(TripViewModel model)
    {
        return new Trip()
        {
            Id = model.Id,
            Number = model.Number,
            EndingAmountOfFuel = model.EndingAmountOfFuel,
            StartingAmountOfFuel = model.StartingAmountOfFuel,
            TotalKM = model.TotalKM,
            StartingTrucKm = model.StartingTrucKm,
            EndingTrucKm = model.EndingTrucKm,
            Employee = new Employee() {Id = model.EmployeeId},
            TruckBase = new TruckBase() {Id = model.TruckBaseId},
            TruckContainer = new TruckContainer() {Id = model.TruckContainerId}
        };
    }

    TripViewModel Parse(Trip model)
    {
        return new TripViewModel()
        {
            Id = model.Id,
            EndingAmountOfFuel = model.EndingAmountOfFuel,
            StartingAmountOfFuel = model.StartingAmountOfFuel,
            TotalKM = model.TotalKM,
            StartingTrucKm = model.StartingTrucKm,
            EndingTrucKm = model.EndingTrucKm,
            Employee = model.Employee.ToString(),
            EmployeeId = model.Employee.Id,
            TruckBase = model.TruckBase.ToString(),
            TruckBaseId = model.TruckBase.Id,
            TruckContainer = model.TruckContainer.ToString(),
            TruckContainerId = model.TruckContainer.Id,
            Number = model.Number
        };
    }

    #endregion
}