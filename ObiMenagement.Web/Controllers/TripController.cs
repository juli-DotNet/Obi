using Microsoft.AspNetCore.Mvc;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;
using ObiMenagement.Web.Models;

namespace ObiMenagement.Web.Controllers;

public class TripController : Controller
{
    private readonly ITripService _tripService;
    private readonly IRoadDataService _roadDataService;

    public TripController(ITripService tripService, IRoadDataService roadDataService)
    {
        _tripService = tripService;
        this._roadDataService = roadDataService;
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

    #region POST

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

    #endregion

    #region RoadData

    public async Task<IActionResult> LoadRoadData(long tripId)
    {
        var result = await _roadDataService.GetAll(tripId);
        var toSendModel = new TableViewModel<RoadDataViewModel>()
        {
            IsSuccessful = result.IsSuccessful,
            ErrorMessage = result.Message
        };
        if (result.IsSuccessful)
        {
            toSendModel.Items = result.Result.Select(ParseRoadData).ToList();
        }

        return Json(toSendModel);
    }

    public async Task<IActionResult> CreateRoadData(int tripId)
    {
        var response = await _tripService.GetByIdAsync(tripId);

        if (!response.IsSuccessful)
        {
            ModelState.AddModelError("", response.Message);
            return View(new RoadDataViewModel());
        }
        var toSendModel = new RoadData
        {
            Trip = response.Result,
            TruckBase = response.Result.TruckBase,
            TruckContainer = response.Result.TruckContainer,
            StartingDate = DateTime.Now
        };

        return View(ParseRoadData(toSendModel));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateRoadData(RoadDataViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.Id = 0;
            var result = await _roadDataService.Create(ParseRoadData(model), model.TripId);
            if (!result.IsSuccessful)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }

            return RedirectToAction("Edit", new { id = model.TripId });
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditRoadData(RoadDataViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _roadDataService.Update(ParseRoadData(model), model.TripId);
            if (!result.IsSuccessful)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }

            return RedirectToAction("Edit", new { id = model.TripId });
        }

        return View(model);
    }

    public async Task<IActionResult> EditRoadData(long id)
    {
        var result = await _roadDataService.GetById(id);

        if (!result.IsSuccessful)
        {
            ModelState.AddModelError("", result.Message);
            return View(new RoadDataViewModel());
        }

        return View(ParseRoadData(result.Result));
    }

    public async Task<IActionResult> DetailsRoadData(long id)
    {
        var result = await _roadDataService.GetById(id);

        if (!result.IsSuccessful)
        {
            ModelState.AddModelError("", result.Message);
            return View(new RoadDataViewModel());
        }

        return View(ParseRoadData(result.Result));
    }

    public async Task<IActionResult> DeleteRoadData(long id)
    {
        var response = await _roadDataService.Delete(id);
        return Json(new GenericViewModel
        {
            IsSuccessful = response.IsSuccessful,
            ErrorMessage = response.Message
        });
    }

    #region Parse
    RoadDataViewModel ParseRoadData(RoadData model)
    {
        return new RoadDataViewModel
        {
            Id = model.Id,
            StartingDate = model.StartingDate.CovertDateToString(),
            EndingDate = model.EndingDate.CovertDateToString(),
            TotalKM = model.TotalKM,
            Price = model.Price,
            IsExport = model.IsExport,
            StartingLocationId = model.StartingLocation?.Id ?? 0,
            StartingLocation = model.StartingLocation?.ToString(),
            DestinationLocation = model.DestinationLocation?.ToString(),
            DestinationLocationId = model.DestinationLocation?.Id ?? 0,
            Trip = model.Trip.ToString(),
            TripId = model.Trip.Id,
            TruckBase = model.TruckBase.ToString(),
            TruckBaseId = model.TruckBase.Id,
            TruckContainer = model.TruckContainer.ToString(),
            TruckContainerId = model.TruckContainer.Id

        };
    }

    RoadData ParseRoadData(RoadDataViewModel model)
    {
        return new RoadData
        {
            Id = model.Id.Value,
            StartingDate = model.StartingDate.ConvertToDate(),
            EndingDate = model.EndingDate.ConvertToDate(),
            TotalKM = model.TotalKM.Value,
            Price = model.Price,
            IsExport = model.IsExport,
            StartingLocation = new Location { Id = model.StartingLocationId },
            DestinationLocation = new Location { Id = model.DestinationLocationId },
            Trip = new Trip { Id = model.TripId },
            TruckBase = new TruckBase { Id = model.TruckBaseId },
            TruckContainer = new TruckContainer { Id = model.TruckContainerId },

        };
    }


    #endregion

    #endregion

    #region Parse

    Trip Parse(TripViewModel model)
    {
        return new Trip()
        {
            Id = model.Id ?? 0,
            Number = model.Number ?? 0,
            EndingAmountOfFuel = model.EndingAmountOfFuel ?? 0,
            StartingAmountOfFuel = model.StartingAmountOfFuel ?? 0,
            TotalKM = model.TotalKM ?? 0,
            StartingTrucKm = model.StartingTrucKm ?? 0,
            EndingTrucKm = model.EndingTrucKm ?? 0,
            Employee = new Employee() { Id = model.EmployeeId },
            TruckBase = new TruckBase() { Id = model.TruckBaseId },
            TruckContainer = new TruckContainer() { Id = model.TruckContainerId },
            TripDate = model.TripDate.ConvertToDate()
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
            Number = model.Number,
            TripDate = model.TripDate.CovertDateToString()
        };
    }

    #endregion
}