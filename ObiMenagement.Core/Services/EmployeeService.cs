using Microsoft.Extensions.Logging;
using ObiMenagement.Core.Common;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;
using System.Linq.Expressions;

namespace ObiMenagement.Core.Services;

public class EmployeeService : BaseService<Employee>, IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeService(IUnitOfWork unitOfWork, ILogger<EmployeeService> logger) : base(unitOfWork, unitOfWork.EmployeeRepository, logger)
    {
        _unitOfWork = unitOfWork;
    }
    protected override async Task<bool> ValidateModel(Employee model, Response result)
    {
        if (model.Person is null)
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.Person)));
            return true;
        }
        if (model.DefaultTruckBase.Id == 0)
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.DefaultTruckBase)));
            return true;
        }
        if (model.DefaultTruckContainer.Id == 0)
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.DefaultTruckContainer)));
            return true;
        }
        model.Person = await _unitOfWork.PersonRepository.FirstOrDefault(a => a.Id == model.Person.Id);
        model.DefaultTruckBase = await _unitOfWork.TruckBaseRepository.FirstOrDefault(a => a.Id == model.DefaultTruckBase.Id);
        model.DefaultTruckContainer = await _unitOfWork.TruckContainerRepository.FirstOrDefault(a => a.Id == model.DefaultTruckContainer.Id);
        model.StartingDate = model.StartingDate.ToUniversalTime();
        if (model.EndingDate.HasValue)
        {
            model.EndingDate = model.EndingDate.Value.ToUniversalTime();
        }

        model.LeaveNote ??= String.Empty;
        return false;
    }

    public async Task<Response<IEnumerable<Employee>>> GetAllWithoutMetadataAsync()
    {
        var result = new Response<IEnumerable<Employee>>();

        try
        {
            result.Result = await _unitOfWork.EmployeeRepository.GetAllAsync();
        }
        catch (Exception e)
        {
            result.Exception = e;
            Logger.Instance.LogError(e);
        }
        return result;
    }

    protected override List<Expression<Func<Employee, object>>> DefaultIncludes()
    {
        return new List<Expression<Func<Employee, object>>> {
          a=>a.Person,
          a=>a.DefaultTruckBase,
          a=>a.DefaultTruckContainer
      };
    }
}