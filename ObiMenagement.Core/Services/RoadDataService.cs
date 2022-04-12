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
        return await RunAsync(async () =>
        {
            var result = new Response();
            if (await ValidateModel(roadData, result))
                throw result.Exception;

            await _unitOfWork.RoadDataRepository.InsertAsync(roadData);
            await _unitOfWork.SaveChangesAsync();

        });
    }
    public async Task<Response> Update(RoadData roadData, long tripId)
    {
        return await RunAsync(async () =>
        {
            var result = new Response();
            if (!await _unitOfWork.PersonRepository.AnyAsync(a => a.Id == roadData.Id && a.IsValid))
            {
                throw new ObiException(ErrorMessages.EntityDoesntExist(roadData.Id));
            }
            if (await ValidateModel(roadData, result))
                throw result.Exception;


            await _unitOfWork.RoadDataRepository.UpdateAsync(roadData);
            await _unitOfWork.SaveChangesAsync();

        });
    }

    public async Task<Response> Delete(long roadDataId)
    {
        return await RunAsync(async () =>
        {
            if (!await _unitOfWork.PersonRepository.AnyAsync(a => a.Id == roadDataId && a.IsValid))
            {
                throw new ObiException(ErrorMessages.EntityDoesntExist(roadDataId));
            }

            await _unitOfWork.RoadDataRepository.DeleteAsync(roadDataId);
            await _unitOfWork.SaveChangesAsync();

        });
    }


    public async Task<Response<List<RoadData>>> GetAll(long tripId)
    {
        return await RunAsync(async () =>
        {
            return await unitOfWork.RoadDataRepository.WhereAsync(a => a.Trip.Id == tripId, a => a.Trip, a => a.TruckContainer, a => a.TruckBase, a => a.StartingLocation, a => a.DestinationLocation);
        });
    }

    public async Task<Response<RoadData>> GetById(long roadDataId)
    {
        return await RunAsync(async () =>
        {
            return await unitOfWork.RoadDataRepository.FirstOrDefault(a => a.Id == roadDataId, a => a.Trip,a=>a.TruckContainer, a => a.TruckBase, a => a.StartingLocation, a => a.DestinationLocation);
        });
    }


    protected override async Task<bool> ValidateModel(RoadData model, Response result)
    {
        if (model.Trip is null || model.Trip.Id == 0)
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.Trip)));
            return true;
        }
        if (model.TruckBase is null || model.TruckBase.Id == 0)
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.TruckBase)));
            return true;
        }
        if (model.TruckContainer is null || model.TruckContainer.Id == 0)
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.TruckContainer)));
            return true;
        }
        model.Trip = await _unitOfWork.TripRepository.FirstOrDefault(a => a.Id == model.Trip.Id);
        model.TruckBase = await _unitOfWork.TruckBaseRepository.FirstOrDefault(a => a.Id == model.TruckBase.Id);
        model.TruckContainer = await _unitOfWork.TruckContainerRepository.FirstOrDefault(a => a.Id == model.TruckContainer.Id);

        if (model.EndingDate == DateTime.MinValue || model.EndingDate < model.Trip.TripDate)
        {
            result.Exception = new ObiException(ErrorMessages.InvalidEntity(nameof(model.EndingDate)));
            return true;
        }

        if (model.TotalKM == 0)
        {
            result.Exception = new ObiException(ErrorMessages.InvalidEntity(nameof(model.TotalKM)));
            return true;
        }
        if (model.Price == 0)
        {
            result.Exception = new ObiException(ErrorMessages.InvalidEntity(nameof(model.Price)));
            return true;
        }

        if (model.StartingLocation is null || model.StartingLocation.Id == 0)
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.StartingLocation)));
            return true;
        }
        if (model.DestinationLocation is null || model.DestinationLocation.Id == 0)
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.DestinationLocation)));
            return true;
        }
        model.DestinationLocation = await _unitOfWork.LocationRepository.FirstOrDefault(a => a.Id == model.DestinationLocation.Id);
        model.StartingLocation = await _unitOfWork.LocationRepository.FirstOrDefault(a => a.Id == model.StartingLocation.Id);
        return false;
    }


}
