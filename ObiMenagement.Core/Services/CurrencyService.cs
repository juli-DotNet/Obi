using Microsoft.Extensions.Logging;
using ObiMenagement.Core.Common;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;
using System.Linq.Expressions;

namespace ObiMenagement.Core.Services;

public class CurrencyService : BaseService<Currency>, ICurrencyService
{
    private readonly IUnitOfWork _unitOfWork;

    public CurrencyService(IUnitOfWork unitOfWork, ILogger<CurrencyService> logger) : base(unitOfWork, unitOfWork.CurrencyRepository, logger)
    {
        _unitOfWork = unitOfWork;
    }
    protected override async Task<bool> ValidateModel(Currency model, Response result)
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
        if (model.Symbol.Length != 1)
        {
            result.Exception = new ObiException(ErrorMessages.InvalidEntity(nameof(model.Symbol)));
            return true;
        }
        if (model.Id == 0 && await _unitOfWork.CurrencyRepository.AnyAsync(a => a.Name == model.Name && a.IsValid))
        {
            result.Exception = new ObiException(ErrorMessages.EntityExist(nameof(model.Name)));
            return true;
        }
        if (model.Country != null && model.Country.Id > 0)
        {
            model.Country = await _unitOfWork.CountryRepository.FirstOrDefault(a => a.Id == model.Country.Id);
        }
        else
        {
            model.Country = null;
        }
        return false;
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

    protected override List<Expression<Func<Currency, object>>> DefaultIncludes()
    {
        return new List<Expression<Func<Currency, object>>> {
       a=>a.Country
       };
    }
}