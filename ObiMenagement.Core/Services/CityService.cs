using ObiMenagement.Core.Common;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Services;

public class CityService : BaseService, ICityService
{
    private readonly IUnitOfWork _unitOfWork;

    public CityService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> CreateAsync(City model)
    {
        var result = new Response();
        if (await ValidateModel(model, result)) return result;

        model.Country = await _unitOfWork.CountryRepository.FirstOrDefault(a => a.Id == model.Country.Id);
        await _unitOfWork.CityRepository.InsertAsync(model);
        await _unitOfWork.SaveChangesAsync();
        return result;
    }

    private async Task<bool> ValidateModel(City model, Response result)
    {
        if (string.IsNullOrWhiteSpace(model.Name))
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.Name)));
            return true;
        }

        if (model.Country is null || model.Country.Id == 0)
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.Country)));
            return true;
        }

        if (await _unitOfWork.CountryRepository.AnyAsync(a => a.Name == model.Name && a.IsValid))
        {
            result.Exception = new ObiException(ErrorMessages.EntityExist(nameof(model.Name)));
            return true;
        }

        return false;
    }

    public async Task<Response> DeleteAsync(int id)
    {
        var result = new Response();
        if (!await _unitOfWork.CityRepository.AnyAsync(a => a.Id ==id && a.IsValid))
        {
            result.Exception = new ObiException(ErrorMessages.EntityDoesntExist(id));
            return result;
        }

        await _unitOfWork.CityRepository.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return result;
    }

    public async Task<Response> EditAsync(City model)
    {
        var result = new Response();
        if (!await _unitOfWork.CountryRepository.AnyAsync(a => a.Id ==model.Id && a.IsValid))
        {
            result.Exception = new ObiException(ErrorMessages.EntityDoesntExist(model.Id));
            return result;
        }
        if (await ValidateModel(model, result)) return result;
        model.Country = await _unitOfWork.CountryRepository.FirstOrDefault(a => a.Id == model.Country.Id);
        await _unitOfWork.CityRepository.UpdateAsync(model);
        await _unitOfWork.SaveChangesAsync();
        return result;
    }

    public async Task<Response<IEnumerable<City>>> GetAllAsync()
    {
        var result = new Response<IEnumerable<City>>();

        result.Result = await _unitOfWork.CityRepository.WhereAsync(a=>true, a=>a.Country);
        return result;
    }
    public async Task<Response<IEnumerable<City>>> GetAllWithoutMetadataAsync(long countryId)
    {
        var result = new Response<IEnumerable<City>>();

        result.Result = await _unitOfWork.CityRepository.WhereAsync(a => a.Country.Id == countryId);
        return result;
    }
    public async Task<Response<City>> GetByIdAsync(int id)
    {
        var result = new Response<City>();
        result.Result = await _unitOfWork.CityRepository.FirstOrDefault(a=>a.Id==id,a=>a.Country);
        return result;
    }
}