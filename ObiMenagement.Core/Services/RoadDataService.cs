using Microsoft.Extensions.Logging;
using ObiMenagement.Core.Common;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Services;

public class RoadDataService : LongBaseService<RoadData>, IRoadDataService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger<TripService> logger;

    public RoadDataService(IUnitOfWork unitOfWork, ILogger<TripService> logger) : base(unitOfWork, unitOfWork.RoadDataRepository, logger)
    {
        this.unitOfWork = unitOfWork;
        this.logger = logger;
    }

    public async Task<Response> Create(RoadData roadData, long tripId)
    {
        throw new NotImplementedException();
    }
    public async Task<Response> Update(RoadData roadData, long tripId)
    {
        throw new NotImplementedException();
    }

    public async Task<Response> Delete(long roadDataId)
    {
        throw new NotImplementedException();
    }


    public async Task<Response<List<RoadData>>> GetAll(long tripId)
    {
        return await RunAsync(async () =>
        {
            return await unitOfWork.RoadDataRepository.WhereAsync(a => a.Trip.Id == tripId, a => a.Trip, a => a.StartingLocation, a => a.DestinationLocation);
        });
    }

    public async Task<Response<RoadData>> GetById(long roadDataId)
    {
        return await RunAsync(async () =>
        {
            return await unitOfWork.RoadDataRepository.FirstOrDefault(a => a.Id == roadDataId, a => a.Trip,a=>a.StartingLocation,a=>a.DestinationLocation);
        });
    }
   

    protected override async Task<bool> ValidateModel(RoadData model, Response result)
    {
        if (model.Trip is null || model.Trip.Id == 0)
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.Trip)));
            return true;
        }
        return false;
    }


}
