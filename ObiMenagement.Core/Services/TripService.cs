using Microsoft.Extensions.Logging;
using ObiMenagement.Core.Common;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Services;

public class TripService : LongBaseService<Trip>, ITripService
{
    public TripService(IUnitOfWork unitOfWork, IRepository<Trip> repository, ILogger logger) : base(unitOfWork, repository, logger)
    {
    }

    protected override async Task<bool> ValidateModel(Trip model, Response result)
    {
        // if (await _unitOfWork.PersonRepository.AnyAsync(a => a.PersonalNumber ==model.PersonalNumber && a.IsValid))
        // {
        //     result.Exception = new ObiException(ErrorMessages.EntityExist(model.PersonalNumber));
        //     return true;
        // }

        return false;
    }

    public async Task<int> CalculateNumber()
    {
        var currentNumber = await _repository.CountAsync(a => true);
        return currentNumber;
    }
}