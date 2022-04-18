using Microsoft.Extensions.Logging;
using ObiMenagement.Core.Common;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;
using System.Linq.Expressions;

namespace ObiMenagement.Core.Services;

public class CityService : BaseService<City>, ICityService
{
    private readonly IUnitOfWork _unitOfWork;

    public CityService(IUnitOfWork unitOfWork,ILogger<CityService> logger):base(unitOfWork,unitOfWork.CityRepository,logger)
    {
        _unitOfWork = unitOfWork;
    }
    protected override async Task<bool> ValidateModel(City model, Response result)
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
        model.Country = await _unitOfWork.CountryRepository.FirstOrDefault(a => a.Id == model.Country.Id);

        return false;
    }

    public async Task<Response<IEnumerable<City>>> GetAllWithoutMetadataAsync(long countryId)
    {
        var result = new Response<IEnumerable<City>>();

        result.Result = await _unitOfWork.CityRepository.WhereAsync(a => a.Country.Id == countryId);
        return result;
    }
 

    protected override List<Expression<Func<City, object>>> DefaultIncludes()
    {
        return new List<Expression<Func<City, object>>>
        {
            a=>a.Country
        };
    }
}