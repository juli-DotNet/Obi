using Microsoft.AspNetCore.Mvc;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;
using ObiMenagement.Web.Models;

namespace ObiMenagement.Web.Controllers;

public class ExpenseTypeController : Controller
{
    private readonly IExpenseTypeService _expenseTypeService;

    public ExpenseTypeController(IExpenseTypeService expenseTypeService)
    {
        this._expenseTypeService = expenseTypeService;
    }
    ExpenseType Parse(ExpenseTypeViewModel model)
    {
        return new ExpenseType
        {
            Id = model.Id,
            Name = model.Name,
            IsFuel = model.IsFuel,
            IsPrepaymentGivenToEmployees = model.IsPrepaymentGivenToEmployee,
            DefaultPayment = new Payment
            {

                PaymentTypeEnum = (PaymentTypeEnum)model.PaymentTypeId,
                Price = model.Price,
                Currency = new Currency { Id = model.CurrencyId }
            }

        };
    }
    ExpenseTypeViewModel Parse(ExpenseType model)
    {
        var result = new ExpenseTypeViewModel()
        {
            Id = model.Id,
            Name = model.Name,
            IsFuel = model.IsFuel,
            IsPrepaymentGivenToEmployee = model.IsPrepaymentGivenToEmployees
        };

        if (model.DefaultPayment is not null && model.DefaultPayment.Currency is not null)
        {
            result.Currency = model.DefaultPayment.Currency.Name;
            result.CurrencySymbol = model.DefaultPayment.Currency.Symbol;
            result.CurrencyId = model.DefaultPayment.Currency.Id;
            result.Price = model.DefaultPayment.Price;
            result.PaymentTypeId = (int)model.DefaultPayment.PaymentTypeEnum;
            result.PaymentType = model.DefaultPayment.PaymentTypeEnum.ToString();
        }

        return result;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _expenseTypeService.GetAllAsync();
        if (!result.IsSuccessful)
        {
            ModelState.AddModelError("", result.Message);
        }
        return View(result.Result.Select(Parse));
    }

    public IActionResult Create()
    {
        var model = new ExpenseTypeViewModel();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ExpenseTypeViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.Id = 0;
            var result = await _expenseTypeService.CreateAsync(Parse(model));
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
        var result = await _expenseTypeService.GetByIdAsync(id);

        if (!result.IsSuccessful)
        {
            ModelState.AddModelError("", result.Message);
            return View(new ExpenseTypeViewModel());
        }

        return View(Parse(result.Result));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ExpenseTypeViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _expenseTypeService.EditAsync(Parse(model));
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
        var response = await _expenseTypeService.GetByIdAsync(id);

        if (!response.IsSuccessful)
        {
            ModelState.AddModelError("", response.Message);
            return View(new ExpenseTypeViewModel());
        }
        return View(Parse(response.Result));
    }
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _expenseTypeService.DeleteAsync(id);
        return Json(new GenericViewModel
        {
            IsSuccessful = response.IsSuccessful,
            ErrorMessage = response.Message
        });
    }
}