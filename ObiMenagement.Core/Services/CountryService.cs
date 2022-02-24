using ObiMenagement.Core.Common;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Services;

public class CountryService : BaseService, ICountryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CountryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response> CreateAsync(Country model)
    {
        var result = new Response();
        if (string.IsNullOrWhiteSpace(model.Name))
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.Name)));
            return result;
        }

        if (await _unitOfWork.CountryRepository.AnyAsync(a => a.Name == model.Name && a.IsValid))
        {
            result.Exception = new ObiException(ErrorMessages.EntityExist(nameof(model.Name)));
            return result;
        }

        await _unitOfWork.CountryRepository.InsertAsync(model);
        await _unitOfWork.SaveChangesAsync();
        return result;
    }

    public async Task<Response> DeleteAsync(int id)
    {
        var result = new Response();
        if (!await _unitOfWork.CountryRepository.AnyAsync(a => a.Id ==id && a.IsValid))
        {
            result.Exception = new ObiException(ErrorMessages.EntityDoesntExist(id));
            return result;
        }

        await _unitOfWork.CountryRepository.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return result;
    }

    public async Task<Response> EditAsync(Country model)
    {
        var result = new Response();
        if (!await _unitOfWork.CountryRepository.AnyAsync(a => a.Id ==model.Id && a.IsValid))
        {
            result.Exception = new ObiException(ErrorMessages.EntityDoesntExist(model.Id));
            return result;
        }

        await _unitOfWork.CountryRepository.UpdateAsync(model);
        await _unitOfWork.SaveChangesAsync();
        return result;
    }

    public async Task<Response<IEnumerable<Country>>> GetAllAsync()
    {
        var result = new Response<IEnumerable<Country>>();

        result.Result = await _unitOfWork.CountryRepository.GetAllAsync();
        return result;
    }

    public async Task<Response<Country>> GetByIdAsync(int id)
    {
        var result = new Response<Country>();
        result.Result = await _unitOfWork.CountryRepository.GetByIdAsync(id);
        return result;
    }

    public async Task<Response<IEnumerable<Country>>> GetAllWithoutMetadataAsync()
    {
        var result = new Response<IEnumerable<Country>>();

        result.Result = await _unitOfWork.CountryRepository.GetAllAsync();
        return result;
    }
}