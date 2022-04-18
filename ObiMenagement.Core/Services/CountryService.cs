using Microsoft.Extensions.Logging;
using ObiMenagement.Core.Common;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;
using System.Linq.Expressions;

namespace ObiMenagement.Core.Services;

public class CountryService : BaseService<Country>, ICountryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CountryService(IUnitOfWork unitOfWork, ILogger<CountryService> logger) : base(unitOfWork, unitOfWork.CountryRepository, logger)
    {
        _unitOfWork = unitOfWork;
    }

    protected override async Task<bool> ValidateModel(Country model, Response result)
    {
        if (string.IsNullOrWhiteSpace(model.Name))
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.Name)));
            return true;
        }
        if (model.Id == 0 && await _unitOfWork.CountryRepository.AnyAsync(a => a.Name == model.Name && a.IsValid))
        {
            result.Exception = new ObiException(ErrorMessages.EntityExist(nameof(model.Name)));
            return true;
        }

        return false;
    }


    public async Task<Response<IEnumerable<Country>>> GetAllWithoutMetadataAsync()
    {
        var result = new Response<IEnumerable<Country>>();

        result.Result = await _unitOfWork.CountryRepository.GetAllAsync();
        return result;
    }

    protected override List<Expression<Func<Country, object>>> DefaultIncludes()
    {
        return new List<Expression<Func<Country, object>>>();
    }
}