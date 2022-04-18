using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using ObiMenagement.Core.Common;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Services;

public class TrackContainerService : BaseService<TruckContainer>, ITrackContainerService
{
    private readonly IUnitOfWork _unitOfWork;

    public TrackContainerService(IUnitOfWork unitOfWork,ILogger<TrackBaseService> logger):base(unitOfWork,unitOfWork.TruckContainerRepository,logger)
    {
        _unitOfWork = unitOfWork;
    }
    
    protected override async Task<bool> ValidateModel(TruckContainer model, Response result)
    {
        if (string.IsNullOrWhiteSpace(model.Plate))
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.Plate)));
            return true;
        }

        return false;
    }

    protected override List<Expression<Func<TruckContainer, object>>> DefaultIncludes()
    {
        return new List<Expression<Func<TruckContainer, object>>>();
    }
}