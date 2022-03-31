using Microsoft.AspNetCore.Mvc;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;
using ObiMenagement.Web.Models;

namespace ObiMenagement.Web.Controllers;

public class TripController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITripService _tripService;

    public TripController(IUnitOfWork unitOfWork, ITripService tripService)
    {
        _unitOfWork = unitOfWork;
        _tripService = tripService;
        tripData = CreateSampleData();
    }

    private List<Trip> tripData = null;

    private List<Trip> CreateSampleData()
    {
        return new List<Trip>()
        {
            new Trip()
            {
                Id = 1,
                Number = 1,
                EndingAmountOfFuel = 1500,
                StartingAmountOfFuel = 500,
                StartingTrucKm = 1000,
                EndingTrucKm = 1500,
                TruckBase = _unitOfWork.TruckBaseRepository.GetAllAsync().GetAwaiter().GetResult().First(),
                Employee = _unitOfWork.EmployeeRepository.GetAllAsync().GetAwaiter().GetResult().First(),
                TruckContainer = _unitOfWork.TruckContainerRepository.GetAllAsync().GetAwaiter().GetResult()
                    .FirstOrDefault(),
                TotalKM = 1200,
                Roads = new List<RoadData>()
                {
                }
            }
        };
    }

    #region Trip CURD

    public async Task<IActionResult> Index()
    {
        // var result = await _trackContainerService.GetAllAsync();
        // if (!result.IsSuccessful)
        // {
        //     ModelState.AddModelError("", result.Message);
        // }
        var result = tripData;
        // return View(result.Result.Select(Parse));
        return View(result.Select(Parse));
    }

    public IActionResult Create()
    {
        var model = new TripViewModel();
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        // var result = await _tripService.GetByIdAsync(id);
        //
        // if (!result.IsSuccessful)
        // {
        //     ModelState.AddModelError("", result.Message);
        //     return View(new TripViewModel());
        // }

        //return View(Parse(result.Result));
        return View(Parse(tripData.First(a => a.Id == id)));
    }

    public async Task<IActionResult> Details(int id)
    {
        // var response = await _tripService.GetByIdAsync(id);
        //
        // if (!response.IsSuccessful)
        // {
        //     ModelState.AddModelError("", response.Message);
        //     return View(new TripViewModel());
        // }
        // return View(Parse(response.Result));
        return View(Parse(tripData.First(a => a.Id == id)));
    }

    public async Task<IActionResult> Delete(int id)
    {
        // var response = await _tripService.DeleteAsync(id);
        // return Json(new GenericViewModel
        // {
        //     IsSuccessful = response.IsSuccessful,
        //     ErrorMessage = response.Message
        // });
        var currentData = tripData.FirstOrDefault(a => a.Id ==id);
        tripData.Remove(currentData);
        return Json(new GenericViewModel
        {
            IsSuccessful = true,
            ErrorMessage = ""
        });
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TripViewModel model)
    {
        if (ModelState.IsValid)
        {
            // model.Id = 0;
            // var result = await _tripService.CreateAsync(Parse(model));
            // if (!result.IsSuccessful)
            // {
            //     ModelState.AddModelError("", result.Message);
            //     return View(model);
            // }
            var modelToInsert = Parse(model);
            modelToInsert.Id = tripData.Count + 1;
            tripData.Add(modelToInsert);
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
            // var result = await _tripService.EditAsync(Parse(model));
            // if (!result.IsSuccessful)
            // {
            //     ModelState.AddModelError("", result.Message);
            //     return View(model);
            //
            // }
            var currentData = tripData.FirstOrDefault(a => a.Id == model.Id);
            if (currentData is null)
            {
                ModelState.AddModelError("", "Entity not found");
                return View(model);
            }
            tripData.Remove(currentData);
            var modelToInsert = Parse(model);
            modelToInsert.Id = model.Id;
            tripData.Add(modelToInsert);
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
            TruckBase = model.TruckBase.Plate,
            TruckBaseId = model.TruckBase.Id,
            TruckContainer = model.TruckContainer.Plate,
            TruckContainerId = model.TruckContainer.Id
        };
    }

    #endregion
}