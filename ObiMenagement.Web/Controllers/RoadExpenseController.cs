using Microsoft.AspNetCore.Mvc;
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
        };
    }

}
