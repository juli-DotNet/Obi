using Microsoft.AspNetCore.Mvc;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;
using ObiMenagement.Web.Models;

namespace ObiMenagement.Web.Controllers;

public class EmployeeController : Controller
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {

        this._employeeService = employeeService;
    }

    #region Parse

    Employee Parse(EmployeeViewModel model)
    {
        return new Employee()
        {
            Id = model.Id,
            EndingDate=model.EndingDate,
            LeaveNote=model.LeaveNote,
            StartingDate=model.StartingDate,
            Person=new Person { Id=model.PersonID},
            DefaultTruckBase=new TruckBase { Id=model.DefaultTruckBaseId}
        };
    }
    EmployeeViewModel Parse(Employee model)
    {
        var result= new EmployeeViewModel()
        {
            Id = model.Id,
            EndingDate=model.EndingDate,
            LeaveNote=model.LeaveNote,
            StartingDate=model.StartingDate
        };
        if (model.DefaultTruckBase is not null)
        {
            result.DefaultTruckBaseId = model.DefaultTruckBase.Id;
            result.DefaultTruckBase = $"{model.DefaultTruckBase.Plate}_{model.DefaultTruckBase.Color}";
        }
        if (model.Person is not null)
        {

            result.Person = $"{model.Person.PersonalNumber}:{model.Person.Name}_{model.Person.LastName}";
            result.PersonID = model.Person.Id;
        }

        return result;
    }


    #endregion
    public async Task<IActionResult> Index()
    {
        var result = await _employeeService.GetAllAsync();
        if (!result.IsSuccessful)
        {
            ModelState.AddModelError("", result.Message);
        }
        return View(result.Result.Select(Parse));
    }

    public IActionResult Create()
    {
        var model = new EmployeeViewModel();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EmployeeViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.Id = 0;
            var result = await _employeeService.CreateAsync(Parse(model));
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
        var result = await _employeeService.GetByIdAsync(id);

        if (!result.IsSuccessful)
        {
            ModelState.AddModelError("", result.Message);
            return View(new EmployeeViewModel());
        }

        return View(Parse(result.Result));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EmployeeViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _employeeService.EditAsync(Parse(model));
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
        var response = await _employeeService.GetByIdAsync(id);

        if (!response.IsSuccessful)
        {
            ModelState.AddModelError("", response.Message);
            return View(new EmployeeViewModel());
        }
        return View(Parse(response.Result));
    }
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _employeeService.DeleteAsync(id);
        return Json(new GenericViewModel
        {
            IsSuccessful = response.IsSuccessful,
            ErrorMessage = response.Message
        });
    }
}
