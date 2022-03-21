using Microsoft.AspNetCore.Mvc;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;
using ObiMenagement.Web.Models;

namespace ObiMenagement.Web.Controllers;

public class ClientController : Controller
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }
    Client Parse(ClientViewModel model)
    {
        return new Client
        {
            Id = model.Id,
            Name = model.Name,
            Notes = model.Notes,
            Location= new Location { Id=model.LocationId}
        };
    }
    ClientViewModel Parse(Client model)
    {
        return new ClientViewModel()
        {
            Id = model.Id,
            Name = model.Name,
            Notes = model.Notes,
            Location = $"{model.Location.Name}:{model.Location.City}:{model.Location.Country}"
        };
    }

    public async Task<IActionResult> Index()
    {
        var result = await _clientService.GetAllAsync();
        if (!result.IsSuccessful)
        {
            ModelState.AddModelError("", result.Message);
        }
        return View(result.Result.Select(Parse));
    }

    public IActionResult Create()
    {
        var model = new ClientViewModel();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ClientViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.Id = 0;
            var result = await _clientService.CreateAsync(Parse(model));
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
        var result = await _clientService.GetByIdAsync(id);

        if (!result.IsSuccessful)
        {
            ModelState.AddModelError("", result.Message);
            return View(new ClientViewModel());
        }

        return View(Parse(result.Result));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ClientViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _clientService.EditAsync(Parse(model));
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
        var response = await _clientService.GetByIdAsync(id);

        if (!response.IsSuccessful)
        {
            ModelState.AddModelError("", response.Message);
            return View(new ClientViewModel());
        }
        return View(Parse(response.Result));
    }
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _clientService.DeleteAsync(id);
        return Json(new GenericViewModel
        {
            IsSuccessful = response.IsSuccessful,
            ErrorMessage = response.Message
        });
    }

    public async Task<IActionResult> GetContacts(int id)
    {
        var response = await _clientService.GetClientContacts(id);

        if (!response.IsSuccessful)
        {
            return Json(new GenericViewModel
            {
                ErrorMessage = response.Message,
                IsSuccessful = response.IsSuccessful
            });
        }
        return Json(new TableViewModel<PersonViewModel>
        {
            IsSuccessful = true,
            Items = response.Result.Select(a => PersonController.Parse(a.Person)).ToList()
        });
    }
}
