using ObiMenagement.Core.Common;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Services;

public class EmployeeService : BaseService, IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    private async Task<bool> ValidateModel(Employee model, Response result)
    {
        if (model.Person is null)
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.Person)));
            return true;
        }

        return false;
    }

    public async Task<Response> CreateAsync(Employee model)
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

    public async Task<Response> DeleteAsync(int id)
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

    public async Task<Response> EditAsync(Employee model)
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

    public async Task<Response<IEnumerable<Employee>>> GetAllAsync()
    {
        var result = new Response<IEnumerable<Employee>>();

        try
        {
            result.Result = await _unitOfWork.EmployeeRepository.WhereAsync(a => true,a=>a.Person);
        }
        catch (Exception e)
        {
            result.Exception = e;
            Logger.Instance.LogError(e);
        }
        return result;
    }

    public async Task<Response<Employee>> GetByIdAsync(int id)
    {
        var result = new Response<Employee>();

        try
        {
            result.Result = await _unitOfWork.EmployeeRepository.FirstOrDefault(a =>a.Id==id,a=>a.Person);
        }
        catch (Exception e)
        {
            result.Exception = e;
            Logger.Instance.LogError(e);
        }
        return result;
    }

    public async Task<Response<IEnumerable<Employee>>> GetAllWithoutMetadataAsync()
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