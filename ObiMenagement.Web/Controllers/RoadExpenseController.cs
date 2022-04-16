using Microsoft.AspNetCore.Mvc;
using ObiMenagement.Core.Common;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;
using ObiMenagement.Web.Models;

namespace ObiMenagement.Web.Controllers;

public class RoadExpenseController : Controller
{
    private readonly IRoadExpenseService _roadExpenseService;

    public RoadExpenseController(IRoadExpenseService roadExpenseService)
    {
        this._roadExpenseService = roadExpenseService;
    }

    public async Task<IActionResult> LoadData(long? tripId, long? roadDataId)
    {
        var toSendModel = new TableViewModel<RoadExpenseViewModel>()
        {
            IsSuccessful = false,

        };
        if (tripId.HasValue)
        {
            toSendModel.IsSuccessful = true;
            var result = await _roadExpenseService.GetAll(tripId.Value);
            toSendModel.Items = result.Result.Select(Parse).ToList();

        }
        else if (roadDataId.HasValue)
        {
            var result = await _roadExpenseService.GetAllByRoad(roadDataId.Value);
            toSendModel.Items = result.Result.Select(Parse).ToList();
            toSendModel.IsSuccessful = true;
        }
        else
        {
            toSendModel.ErrorMessage = ErrorMessages.InvalidRequest("TripdId");
        }
        return Json(toSendModel);
    }

    public async Task<IActionResult> Create(long? tripId, long? roadDataId)
    {
        return View(new RoadExpenseViewModel()
        {
            RoadDataId = roadDataId,
            TripId = tripId ?? 0
        });
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RoadExpenseViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.Id = 0;
            var result = await _roadExpenseService.Create(Parse(model));
            if (!result.IsSuccessful)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }
            if (model.RoadDataId > 0)
                return RedirectToAction("EditRoadData", "Trip", new { id = model.RoadDataId });

            return RedirectToAction("Edit", "Trip", new { id = model.TripId });

        }

        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(RoadExpenseViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _roadExpenseService.Update(Parse(model));
            if (!result.IsSuccessful)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }
            if (model.RoadDataId > 0)
                return RedirectToAction("EditRoadData", "Trip", new { id = model.RoadDataId });

            return RedirectToAction("Edit", "Trip", new { id = model.TripId });

        }

        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var result = await _roadExpenseService.GetById(id);

        if (!result.IsSuccessful)
        {
            ModelState.AddModelError("", result.Message);
            return View(new RoadExpenseViewModel());
        }

        return View(Parse(result.Result));
    }

    public async Task<IActionResult> Details(int id)
    {
        var response = await _roadExpenseService.GetById(id);

        if (!response.IsSuccessful)
        {
            ModelState.AddModelError("", response.Message);
            return View(new RoadExpenseViewModel());
        }

        return View(Parse(response.Result));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var response = await _roadExpenseService.Delete(id);
        return Json(new GenericViewModel
        {
            IsSuccessful = response.IsSuccessful,
            ErrorMessage = response.Message
        });
    }
    RoadExpense Parse(RoadExpenseViewModel model)
    {
        return new RoadExpense
        {
            Id = model.Id ?? 0,
            Name = model.Name,
            IsValid = true,
            Trip = new Trip { Id = model.TripId },
            RoadData = new RoadData { Id = model.RoadDataId ?? 0 },
            ExpenseType = new ExpenseType { Id = model.ExpenseTypeId },
            Quantity = model.Quantity,
            Price = model.Price,
            Note = model.Note,
            Country = new Country { Id = model.CountryId ?? 0 },
            Payment = new Payment
            {
                Currency = new Currency { Id = model.CurrencyId },
                PaymentTypeEnum = (PaymentTypeEnum)model.PaymentTypeId
            }
        };
    }
    RoadExpenseViewModel Parse(RoadExpense model)
    {
        return new RoadExpenseViewModel()
        {
            Id = model.Id,
            Name = model.Name,
            Country = model.Country?.ToString() ?? "",
            CountryId = model.Country?.Id ?? 0,
            TripId = model.Trip.Id,
            Currency = model.Payment.Currency.ToString(),
            CurrencyId = model.Payment.Currency.Id,
            PaymentTypeId = (int)model.Payment.PaymentTypeEnum,
            PaymentType = model.Payment.PaymentTypeEnum.ToString(),
            ExpenseType = model.ExpenseType.ToString(),
            Quantity = model.Quantity,
            ExpenseTypeId = model.ExpenseType.Id,
            Note = model.Note,
            Price = model.Price,
            RoadDataId = model.RoadData?.Id ?? 0
        };
    }

}
