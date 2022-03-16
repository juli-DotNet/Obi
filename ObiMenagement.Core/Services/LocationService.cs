using Microsoft.Extensions.Logging;
using ObiMenagement.Core.Common;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Services;

public class LocationService : BaseService<Location>, ILocationService
{
    private readonly IUnitOfWork _unitOfWork;

    public LocationService(IUnitOfWork unitOfWork,ILogger<LocationService> logger):base(unitOfWork,unitOfWork.LocationRepository,logger)
    {
        _unitOfWork = unitOfWork;
    }
    protected override async Task<bool> ValidateModel(Location model, Response result)
    {
        if (string.IsNullOrWhiteSpace(model.Name))
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.Name)));
            return true;
        }

        if (model.Country == null && model.Country.Id == 0)
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.Country)));
            return true;
        }
        model.Country = await _unitOfWork.CountryRepository.FirstOrDefault(a => a.Id == model.Country.Id);
       
        if (model.City == null && model.City.Id == 0)
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.City)));
            return true;
        }
        model.City = await _unitOfWork.CityRepository.FirstOrDefault(a => a.Id == model.City.Id);
        return false;
    }
    public async Task<Response> CreateAsync(Location model)
    {
        var result = new Response();
        try
        {
            if (await ValidateModel(model, result)) return result;

            await _unitOfWork.LocationRepository.InsertAsync(model);
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
            if (!await _unitOfWork.LocationRepository.AnyAsync(a => a.Id ==id && a.IsValid))
            {
                result.Exception = new ObiException(ErrorMessages.EntityDoesntExist(id));
                return result;
            }

            await _unitOfWork.LocationRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Logger.Instance.LogError(e);
            result.Exception = e;
        }

        return result;
    }

    public async Task<Response> EditAsync(Location model)
    {
        var result = new Response();
        try
        {
            if (!await _unitOfWork.LocationRepository.AnyAsync(a => a.Id ==model.Id && a.IsValid))
            {
                result.Exception = new ObiException(ErrorMessages.EntityDoesntExist(model.Id));
                return result;
            }
            if (await ValidateModel(model, result)) return result;

            await _unitOfWork.LocationRepository.UpdateAsync(model);
            await _unitOfWork.SaveChangesAsync();

        }
        catch (Exception e)
        {
            Logger.Instance.LogError(e);
            result.Exception = e;
        }

        return result;
    }

    public async Task<Response<IEnumerable<Location>>> GetAllAsync()
    {
        var result = new Response<IEnumerable<Location>>();

        try
        {
            result.Result = await _unitOfWork.LocationRepository.WhereAsync(a => true, 
                a => a.Country,a=>a.City);
        }
        catch (Exception e)
        {
            result.Exception = e;
            Logger.Instance.LogError(e);
        }
        return result;
    }

    public async Task<Response<Location>> GetByIdAsync(int id)
    {
        var result = new Response<Location>();

        try
        {
            result.Result = await _unitOfWork.LocationRepository.FirstOrDefault(a => a.Id==id, 
                a => a.Country,a=>a.City);
        }
        catch (Exception e)
        {
            result.Exception = e;
            Logger.Instance.LogError(e);
        }
        return result;
    }

    public async Task<Response<IEnumerable<Location>>> GetAllWithoutMetadataAsync()
    {
        var result = new Response<IEnumerable<Location>>();

        try
        {
            result.Result = await _unitOfWork.LocationRepository.GetAllAsync();
        }
        catch (Exception e)
        {
            result.Exception = e;
            Logger.Instance.LogError(e);
        }
        return result;
    }
}