using Microsoft.Extensions.Logging;
using ObiMenagement.Core.Common;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Services;

public class RoadClientService : LongBaseService<RoadClient>, IRoadClientService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger logger;

    public RoadClientService(IUnitOfWork unitOfWork, ILogger<RoadClientService> logger):base(unitOfWork,unitOfWork.RoadClientRepository,logger)
    {
        this.unitOfWork = unitOfWork;
        this.logger = logger;
    }

    protected override async Task<bool> ValidateModel(RoadClient model, Response result)
    {
        if (model.Client is null)
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.Client)));
            return true;
        }

        if (model.Currency is null )
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.Currency)));
            return true;
        }
        if (model.DestinationLocation is null)
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.DestinationLocation)));
            return true;
        }
        return false;
    }
}