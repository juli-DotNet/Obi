using System.Linq.Expressions;
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

    protected override List<Expression<Func<Location, object>>> DefaultIncludes()
    {
        return new List<Expression<Func<Location, object>>>()
        {
            a => a.Country, a => a.City
        };
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