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
        };
    }
    ExpenseTypeViewModel Parse(ExpenseType model)
    {
        return new ExpenseTypeViewModel()
        {
            Id = model.Id,
        };
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