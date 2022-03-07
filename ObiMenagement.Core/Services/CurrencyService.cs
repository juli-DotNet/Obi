using ObiMenagement.Core.Common;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Services;

public class CurrencyService : BaseService, ICurrencyService
{
    private readonly IUnitOfWork _unitOfWork;

    public CurrencyService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    private async Task<bool> ValidateModel(Currency model, Response result)
    {
        if (string.IsNullOrWhiteSpace(model.Name))
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.Name)));
            return true;
        }
        if (string.IsNullOrWhiteSpace(model.Symbol))
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.Symbol)));
            return true;
        }
        if (model.Symbol.Length!=1)
        {
            result.Exception = new ObiException(ErrorMessages.InvalidEntity(nameof(model.Symbol)));
            return true;
        }


        if (await _unitOfWork.CurrencyRepository.AnyAsync(a => a.Name == model.Name && a.IsValid))
        {
            result.Exception = new ObiException(ErrorMessages.EntityExist(nameof(model.Name)));
            return true;
        }

        return false;
    }
    public async Task<Response> CreateAsync(Currency model)
    {
        var result = new Response();
        try
        {
            if (await ValidateModel(model, result)) return result;
            if (model.Country!=null && model.Country.Id>0)
            {
                model.Country = await _unitOfWork.CountryRepository.FirstOrDefault(a => a.Id == model.Country.Id);
            }
            else            
            {
                model.Country = null;
            }
           
            await _unitOfWork.CurrencyRepository.InsertAsync(model);
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
            if (!await _unitOfWork.CurrencyRepository.AnyAsync(a => a.Id ==id && a.IsValid))
            {
                result.Exception = new ObiException(ErrorMessages.EntityDoesntExist(id));
                return result;
            }

            await _unitOfWork.CurrencyRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Logger.Instance.LogError(e);
            result.Exception = e;
        }

        return result;
    }

    public async Task<Response> EditAsync(Currency model)
    {
        var result = new Response();

        try
        {
            if (await ValidateModel(model, result)) return result;
            if (!await _unitOfWork.CurrencyRepository.AnyAsync(a => a.Id ==model.Id && a.IsValid))
            {
                result.Exception = new ObiException(ErrorMessages.EntityDoesntExist(model.Id));
                return result;
            }
            if (model.Country!=null && model.Country.Id>0)
            {
                model.Country = await _unitOfWork.CountryRepository.FirstOrDefault(a => a.Id == model.Country.Id);
            }
            else            
            {
                model.Country = null;
            }
            await _unitOfWork.CurrencyRepository.UpdateAsync(model);
            await _unitOfWork.SaveChangesAsync();
            
        }
        catch (Exception e)
        {
           Logger.Instance.LogError(e);
           result.Exception = e;
        }

        return result;
    }

    public async Task<Response<IEnumerable<Currency>>> GetAllAsync()
    {
        var result = new Response<IEnumerable<Currency>>();

        try
        {
            result.Result = await _unitOfWork.CurrencyRepository.WhereAsync(a => true, a => a.Country);
        }
        catch (Exception e)
        {
            result.Exception = e;
            Logger.Instance.LogError(e);
        }
        return result;
    }

    public async Task<Response<Currency>> GetByIdAsync(int id)
    {
        var result = new Response<Currency>();

        try
        {
            result.Result = await _unitOfWork.CurrencyRepository.FirstOrDefault(a => a.Id==id, a => a.Country);
        }
        catch (Exception e)
        {
            result.Exception = e;
            Logger.Instance.LogError(e);
        }
        return result;
    }

    public async Task<Response<IEnumerable<Currency>>> GetAllWithoutMetadataAsync()
    {
        var result = new Response<IEnumerable<Currency>>();

        try
        {
            result.Result = await _unitOfWork.CurrencyRepository.GetAllAsync();
        }
        catch (Exception e)
        {
            result.Exception = e;
            Logger.Instance.LogError(e);
        }
        return result;
    }
}