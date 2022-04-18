using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using ObiMenagement.Core.Common;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Services;

public class PersonService : BaseService<Person>, IPersonService
{
    private readonly IUnitOfWork _unitOfWork;

    protected override async Task<bool> ValidateModel(Person model, Response result)
    {
        if (string.IsNullOrWhiteSpace(model.Name))
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.Name)));
            return true;
        }
        if (string.IsNullOrWhiteSpace(model.LastName))
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.LastName)));
            return true;
        }
        if (string.IsNullOrWhiteSpace(model.PhoneNumber))
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.PhoneNumber)));
            return true;
        }
        if (string.IsNullOrWhiteSpace(model.PersonalNumber))
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.PersonalNumber)));
            return true;
        }
        if (await _unitOfWork.PersonRepository.AnyAsync(a => a.PersonalNumber ==model.PersonalNumber && a.IsValid))
        {
            result.Exception = new ObiException(ErrorMessages.EntityExist(model.PersonalNumber));
            return true;
        }

        return false;
    }

    protected override List<Expression<Func<Person, object>>> DefaultIncludes()
    {
        return new List<Expression<Func<Person, object>>>();
    }

    public PersonService(IUnitOfWork unitOfWork,ILogger<PersonService> logger):base(unitOfWork,unitOfWork.PersonRepository,logger)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<IEnumerable<Person>>> GetAllWithoutMetadataAsync()
    {
        var result = new Response<IEnumerable<Person>>();

        try
        {
            result.Result = await _unitOfWork.PersonRepository.GetAllAsync();
        }
        catch (Exception e)
        {
            result.Exception = e;
            Logger.Instance.LogError(e);
        }
        return result;
    }
}