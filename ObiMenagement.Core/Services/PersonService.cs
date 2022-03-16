using Microsoft.Extensions.Logging;
using ObiMenagement.Core.Common;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Services;

public class PersonService : BaseService<Person>, IPersonService
{
    private readonly IUnitOfWork _unitOfWork;

    protected override async Task<bool> ValidateModel(Person model, Response result)
    {
        if (string.IsNullOrWhiteSpace(model.Name))
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.Name)));
            return true;
        }
        if (string.IsNullOrWhiteSpace(model.LastName))
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.LastName)));
            return true;
        }
        if (string.IsNullOrWhiteSpace(model.PhoneNumber))
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.PhoneNumber)));
            return true;
        }
        if (string.IsNullOrWhiteSpace(model.PersonalNumber))
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.PersonalNumber)));
            return true;
        }
        if (await _unitOfWork.PersonRepository.AnyAsync(a => a.PersonalNumber ==model.PersonalNumber && a.IsValid))
        {
            result.Exception = new ObiException(ErrorMessages.EntityExist(model.PersonalNumber));
            return true;
        }

        return false;
    }
    public PersonService(IUnitOfWork unitOfWork,ILogger<PersonService> logger):base(unitOfWork,unitOfWork.PersonRepository,logger)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> CreateAsync(Person model)
    {
        var result = new Response();
        try
        {
            if (await ValidateModel(model, result)) return result;

            await _unitOfWork.PersonRepository.InsertAsync(model);
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
            if (!await _unitOfWork.PersonRepository.AnyAsync(a => a.Id ==id && a.IsValid))
            {
                result.Exception = new ObiException(ErrorMessages.EntityDoesntExist(id));
                return result;
            }

            await _unitOfWork.PersonRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Logger.Instance.LogError(e);
            result.Exception = e;
        }

        return result;
    }

    public async Task<Response> EditAsync(Person model)
    {
        var result = new Response();
        try
        {
            if (!await _unitOfWork.PersonRepository.AnyAsync(a => a.Id ==model.Id && a.IsValid))
            {
                result.Exception = new ObiException(ErrorMessages.EntityDoesntExist(model.Id));
                return result;
            }
            if (await ValidateModel(model, result)) return result;

            await _unitOfWork.PersonRepository.UpdateAsync(model);
            await _unitOfWork.SaveChangesAsync();

        }
        catch (Exception e)
        {
            Logger.Instance.LogError(e);
            result.Exception = e;
        }

        return result;
    }

    public async Task<Response<IEnumerable<Person>>> GetAllAsync()
    {
        var result = new Response<IEnumerable<Person>>();

        try
        {
            result.Result = await _unitOfWork.PersonRepository.WhereAsync(a => true);
        }
        catch (Exception e)
        {
            result.Exception = e;
            Logger.Instance.LogError(e);
        }
        return result;
    }

    public async Task<Response<Person>> GetByIdAsync(int id)
    {
        var result = new Response<Person>();

        try
        {
            result.Result = await _unitOfWork.PersonRepository.FirstOrDefault(a => a.Id==id);
        }
        catch (Exception e)
        {
            result.Exception = e;
            Logger.Instance.LogError(e);
        }
        return result;
    }

    public async Task<Response<IEnumerable<Person>>> GetAllWithoutMetadataAsync()
    {
        var result = new Response<IEnumerable<Person>>();

        try
        {
            result.Result = await _unitOfWork.PersonRepository.GetAllAsync();
        }
        catch (Exception e)
        {
            result.Exception = e;
            Logger.Instance.LogError(e);
        }
        return result;
    }
}