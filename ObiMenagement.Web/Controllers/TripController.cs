using Microsoft.AspNetCore.Mvc;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;
using ObiMenagement.Web.Models;

namespace ObiMenagement.Web.Controllers;

public class TripController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public TripController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        tripData = CreateSampleData();
    }

    #region Parse

    Trip Parse(TripViewModel model)
    {
        return new Trip() { };
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

    // public IActionResult Create()
    // {
    //     var model = new TruckContainerViewModel();
    //     return View(model);
    // }
    //
    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public async Task<IActionResult> Create(TruckContainerViewModel model)
    // {
    //     if (ModelState.IsValid)
    //     {
    //         model.Id = 0;
    //         var result = await _trackContainerService.CreateAsync(Parse(model));
    //         if (!result.IsSuccessful)
    //         {
    //             ModelState.AddModelError("", result.Message);
    //             return View(model);
    //         }
    //         return RedirectToAction("Index");
    //     }
    //
    //
    //     return View(model);
    // }
    //
    // public async Task<IActionResult> Edit(int id)
    // {
    //     var result = await _trackContainerService.GetByIdAsync(id);
    //
    //     if (!result.IsSuccessful)
    //     {
    //         ModelState.AddModelError("", result.Message);
    //         return View(new TruckContainerViewModel());
    //     }
    //
    //     return View(Parse(result.Result));
    // }
    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public async Task<IActionResult> Edit(TruckContainerViewModel model)
    // {
    //     if (ModelState.IsValid)
    //     {
    //         var result = await _trackContainerService.EditAsync(Parse(model));
    //         if (!result.IsSuccessful)
    //         {
    //             ModelState.AddModelError("", result.Message);
    //             return View(model);
    //
    //         }
    //         return RedirectToAction("Index");
    //     }
    //     return View(model);
    //
    // }
    // public async Task<IActionResult> Details(int id)
    // {
    //     var response = await _trackContainerService.GetByIdAsync(id);
    //
    //     if (!response.IsSuccessful)
    //     {
    //         ModelState.AddModelError("", response.Message);
    //         return View(new TruckContainerViewModel());
    //     }
    //     return View(Parse(response.Result));
    // }
    // public async Task<IActionResult> Delete(int id)
    // {
    //     var response = await _trackContainerService.DeleteAsync(id);
    //     return Json(new GenericViewModel
    //     {
    //         IsSuccessful = response.IsSuccessful,
    //         ErrorMessage = response.Message
    //     });
    // }
}