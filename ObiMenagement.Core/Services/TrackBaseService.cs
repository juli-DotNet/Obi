using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using ObiMenagement.Core.Common;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Services;

public class TrackBaseService : BaseService<TruckBase>, ITrackBaseService
{
    private readonly IUnitOfWork _unitOfWork;

    protected override async Task<bool> ValidateModel(TruckBase model, Response result)
    {
        if (string.IsNullOrWhiteSpace(model.Plate))
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.Plate)));
            return true;
        }
        return false;
    }

    protected override List<Expression<Func<TruckBase, object>>> DefaultIncludes()
    {
        return new List<Expression<Func<TruckBase, object>>>();
    }

    public TrackBaseService(IUnitOfWork unitOfWork,ILogger<TrackBaseService> logger):base(unitOfWork,unitOfWork.TruckBaseRepository,logger)
    {
        _unitOfWork = unitOfWork;
    }

}