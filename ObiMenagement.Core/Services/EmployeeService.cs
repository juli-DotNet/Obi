using Microsoft.Extensions.Logging;
using ObiMenagement.Core.Common;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Services;

public class EmployeeService : BaseService<Employee>, IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeService(IUnitOfWork unitOfWork,ILogger<EmployeeService> logger):base(unitOfWork,unitOfWork.EmployeeRepository,logger)
    {
        _unitOfWork = unitOfWork;
    }
    protected override async Task<bool> ValidateModel(Employee model, Response result)
    {
        if (model.Person is null)
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.Person)));
            return true;
        }
        if (model.DefaultTruckBase.Id==0)
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.DefaultTruckBase)));
            return true;
        }
        if (model.DefaultTruckContainer.Id == 0)
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.DefaultTruckContainer)));
            return true;
        }
        model.Person = await _unitOfWork.PersonRepository.FirstOrDefault(a => a.Id == model.Person.Id);
        model.DefaultTruckBase = await _unitOfWork.TruckBaseRepository.FirstOrDefault(a => a.Id == model.DefaultTruckBase.Id);
        model.DefaultTruckContainer = await _unitOfWork.TruckContainerRepository.FirstOrDefault(a => a.Id == model.DefaultTruckContainer.Id);
        model.StartingDate = model.StartingDate.ToUniversalTime();
        if (model.EndingDate.HasValue)
        {
            model.EndingDate = model.EndingDate.Value.ToUniversalTime();
        }

        model.LeaveNote ??= String.Empty;
        return false;
    }

    public override async Task<Response> CreateAsync(Employee model)
    {
        var result = new Response();
        try
        {
            if (await ValidateModel(model, result)) return result;
            
            await _unitOfWork.EmployeeRepository.InsertAsync(model);
            await _unitOfWork.SaveChangesAsync();

        }
        catch (Exception e)
        {
            Logger.Instance.LogError(e);
            result.Exception = e;
        }

        return result;
    }

    public override async Task<Response> DeleteAsync(int id)
    {
        var result = new Response();

        try
        {
            if (!await _unitOfWork.EmployeeRepository.AnyAsync(a => a.Id ==id && a.IsValid))
            {
                result.Exception = new ObiException(ErrorMessages.EntityDoesntExist(id));
                return result;
            }

            await _unitOfWork.EmployeeRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Logger.Instance.LogError(e);
            result.Exception = e;
        }

        return result;
    }

    public override async Task<Response> EditAsync(Employee model)
    {
        var result = new Response();
        try
        {
            if (!await _unitOfWork.EmployeeRepository.AnyAsync(a => a.Id ==model.Id && a.IsValid))
            {
                result.Exception = new ObiException(ErrorMessages.EntityDoesntExist(model.Id));
                return result;
            }
            if (await ValidateModel(model, result)) return result;

            await _unitOfWork.EmployeeRepository.UpdateAsync(model);
            await _unitOfWork.SaveChangesAsync();

        }
        catch (Exception e)
        {
            Logger.Instance.LogError(e);
            result.Exception = e;
        }

        return result;
    }

    public override async Task<Response<IEnumerable<Employee>>> GetAllAsync(string search=null)
    {
        var result = new Response<IEnumerable<Employee>>();

        try
        {
            result.Result = await _unitOfWork.EmployeeRepository.WhereAsync(a => true,a=>a.Person,a=>a.DefaultTruckBase, a => a.DefaultTruckContainer);
        }
        catch (Exception e)
        {
            result.Exception = e;
            Logger.Instance.LogError(e);
        }
        return result;
    }

    public override async Task<Response<Employee>> GetByIdAsync(int id)
    {
        var result = new Response<Employee>();

        try
        {
            result.Result = await _unitOfWork.EmployeeRepository.FirstOrDefault(a =>a.Id==id,a=>a.Person,a=>a.DefaultTruckBase,a=>a.DefaultTruckContainer);
        }
        catch (Exception e)
        {
            result.Exception = e;
            Logger.Instance.LogError(e);
        }
        return result;
    }

    public  async Task<Response<IEnumerable<Employee>>> GetAllWithoutMetadataAsync()
    {
        var result = new Response<IEnumerable<Employee>>();

        try
        {
            result.Result = await _unitOfWork.EmployeeRepository.GetAllAsync();
        }
        catch (Exception e)
        {
            result.Exception = e;
            Logger.Instance.LogError(e);
        }
        return result;
    }
}