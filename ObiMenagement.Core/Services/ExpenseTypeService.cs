using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using ObiMenagement.Core.Common;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Services;

public class ExpenseTypeService : BaseService<ExpenseType>, IExpenseTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;

    public ExpenseTypeService(IUnitOfWork unitOfWork, ILogger<ExpenseTypeService> logger) : base(unitOfWork,
        unitOfWork.ExpenseTypeRepository, logger)
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

        if (model.DefaultPayment is null || model.DefaultPayment.Currency is null)
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.DefaultPayment)));
            return true;
        }

        model.DefaultPayment.Currency =
            await _unitOfWork.CurrencyRepository.FirstOrDefault(a => a.Id == model.DefaultPayment.Currency.Id);
        return false;
    }

    protected override List<Expression<Func<ExpenseType, object>>> DefaultIncludes()
    {
        return new List<Expression<Func<ExpenseType, object>>>()
        {
            a => a.DefaultPayment, a => a.DefaultPayment.Currency
        };
    }


    public override async Task<Response> EditAsync(ExpenseType model)
    {
        var result = new Response();
        try
        {
            var currentData =
                await _unitOfWork.ExpenseTypeRepository.FirstOrDefault(a => a.Id == model.Id && a.IsValid);
            if (currentData is null)
            {
                result.Exception = new ObiException(ErrorMessages.EntityDoesntExist(model.Id));
                return result;
            }

            if (await ValidateModel(model, result)) return result;

            await _unitOfWork.ExpenseTypeRepository.DeleteAsync(currentData.Id);
            await _unitOfWork.SaveChangesAsync();

            model.Id = 0;
            await _unitOfWork.ExpenseTypeRepository.InsertAsync(model);
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
}