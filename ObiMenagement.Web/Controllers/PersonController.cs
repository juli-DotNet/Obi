using Microsoft.AspNetCore.Mvc;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;
using ObiMenagement.Web.Models;

namespace ObiMenagement.Web.Controllers;

public class PersonController : Controller
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        this._personService = personService;
    }

    #region Parse

    Person Parse(PersonViewModel model)
    {
        return new Person()
        {
            Id = model.Id,
            Name=model.Name,
            BirthDay=model.BirthDay,
            DrivingLicenceExpiringDate=model.DrivingLicenceExpiringDate,
            Email=model.Email,
            IsValid=true,
            LastName=model.LastName,
            PassportExpiringDate=model.PassportExpiringDate,
            PersonalNumber=model.PersonalNumber,
            PhoneNumber=model.PhoneNumber
        };
    }
    PersonViewModel Parse(Person model)
    {
        return new PersonViewModel()
        {
            Id = model.Id,
            Name = model.Name,
            BirthDay = model.BirthDay,
            DrivingLicenceExpiringDate = model.DrivingLicenceExpiringDate,
            Email = model.Email,
            LastName = model.LastName,
            PassportExpiringDate = model.PassportExpiringDate,
            PersonalNumber = model.PersonalNumber,
            PhoneNumber = model.PhoneNumber
        };
    }


    #endregion
    public async Task<IActionResult> Index()
    {
        var result = await _personService.GetAllAsync();
        if (!result.IsSuccessful)
        {
            ModelState.AddModelError("", result.Message);
        }
        return View(result.Result.Select(Parse));
    }

    public IActionResult Create()
    {
        var model = new PersonViewModel();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PersonViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.Id = 0;
            var result = await _personService.CreateAsync(Parse(model));
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
        var result = await _personService.GetByIdAsync(id);

        if (!result.IsSuccessful)
        {
            ModelState.AddModelError("", result.Message);
            return View(new PersonViewModel());
        }

        return View(Parse(result.Result));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(PersonViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _personService.EditAsync(Parse(model));
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
        var response = await _personService.GetByIdAsync(id);

        if (!response.IsSuccessful)
        {
            ModelState.AddModelError("", response.Message);
            return View(new PersonViewModel());
        }
        return View(Parse(response.Result));
    }
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _personService.DeleteAsync(id);
        return Json(new GenericViewModel
        {
            IsSuccessful = response.IsSuccessful,
            ErrorMessage = response.Message
        });
    }
}
