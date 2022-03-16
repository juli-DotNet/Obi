using Microsoft.Extensions.Logging;
using ObiMenagement.Core.Common;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Services;

public class ExpenseTypeService : BaseService<ExpenseType>, IExpenseTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;

    public ExpenseTypeService(IUnitOfWork unitOfWork,ILogger<ExpenseTypeService> logger):base(unitOfWork,unitOfWork.ExpenseTypeRepository,logger)
    {
        this._unitOfWork = unitOfWork;
        this._logger = logger;
    }

    protected override async Task<bool> ValidateModel(ExpenseType model, Response result)
    {
        if (string.IsNullOrWhiteSpace(model.Name))
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.Name)));
            return true;
        }

        if (model.DefaultPayment is null||model.DefaultPayment.Currency is null)
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.DefaultPayment)));
            return true;
        }
        return false;
    }
    public async Task<Response> CreateAsync(ExpenseType model)
    {
        var result = new Response();
        try
        {
            if (await ValidateModel(model, result)) return result;

            await _unitOfWork.ExpenseTypeRepository.InsertAsync(model);
            await _unitOfWork.SaveChangesAsync();

        }
        catch (Exception e)
        {
            Logger.Instance.LogError(e);
            _logger.LogError(e, "failed to create the model");
            result.Exception = e;
        }

        return result;
    }

    public async Task<Response> DeleteAsync(int id)
    {
        var result = new Response();

        try
        {
            if (!await _unitOfWork.ExpenseTypeRepository.AnyAsync(a => a.Id == id && a.IsValid))
            {
                result.Exception = new ObiException(ErrorMessages.EntityDoesntExist(id));
                return result;
            }

            await _unitOfWork.TruckBaseRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "failed to delete the model");
            Logger.Instance.LogError(e);
            result.Exception = e;
        }

        return result;
    }

    public async Task<Response> EditAsync(ExpenseType model)
    {
        var result = new Response();
        try
        {
            if (!await _unitOfWork.ExpenseTypeRepository.AnyAsync(a => a.Id == model.Id && a.IsValid))
            {
                result.Exception = new ObiException(ErrorMessages.EntityDoesntExist(model.Id));
                return result;
            }
            if (await ValidateModel(model, result)) return result;

            await _unitOfWork.ExpenseTypeRepository.UpdateAsync(model);
            await _unitOfWork.SaveChangesAsync();

        }
        catch (Exception e)
        {
            _logger.LogError(e, "failed to delete the model");
            Logger.Instance.LogError(e);
            result.Exception = e;
        }

        return result;
    }

    public async Task<Response<IEnumerable<ExpenseType>>> GetAllAsync()
    {
        var result = new Response<IEnumerable<ExpenseType>>();

        try
        {
            result.Result = await _unitOfWork.ExpenseTypeRepository.WhereAsync(a => true);
        }
        catch (Exception e)
        {
            result.Exception = e;
            Logger.Instance.LogError(e);
        }
        return result;
    }

    public async Task<Response<ExpenseType>> GetByIdAsync(int id)
    {
        var result = new Response<ExpenseType>();

        try
        {
            result.Result = await _unitOfWork.ExpenseTypeRepository.FirstOrDefault(a => a.Id == id);
        }
        catch (Exception e)
        {
            result.Exception = e;
            Logger.Instance.LogError(e);
        }
        return result;
    }


}
