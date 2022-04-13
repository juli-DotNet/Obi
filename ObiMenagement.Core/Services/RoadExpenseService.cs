using Microsoft.Extensions.Logging;
using ObiMenagement.Core.Common;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;
using System.Linq.Expressions;

namespace ObiMenagement.Core.Services;

public class RoadExpenseService : LongBaseService<RoadExpense>, IRoadExpenseService
{
    public RoadExpenseService(IUnitOfWork unitOfWork, ILogger<RoadExpense> logger) :
        base(unitOfWork, unitOfWork.RoadExpenseRepository, logger)
    {

    }

    public async Task<Response> Create(RoadExpense roadData)
    {
        return await RunAsync(async () =>
        {
            var result = new Response();
            if (await ValidateModel(roadData, result)) throw result.Exception;

            await _unitOfWork.RoadExpenseRepository.InsertAsync(roadData);
            await _unitOfWork.SaveChangesAsync();

        });
    }

    public async Task<Response> Delete(long id)
    {
        return await RunAsync(async () =>
        {
            if (!await _unitOfWork.RoadExpenseRepository.AnyAsync(a => a.Id == id && a.IsValid))
            {
                throw new ObiException(ErrorMessages.EntityDoesntExist(id));
            }
            await _unitOfWork.RoadExpenseRepository.DeleteAsync(id);

        });
    }


    public async Task<Response> Update(RoadExpense roadData)
    {
        return await RunAsync(async () =>
        {
            if (!await _unitOfWork.RoadExpenseRepository.AnyAsync(a => a.Id == roadData.Id && a.IsValid))
            {
                throw new ObiException(ErrorMessages.EntityDoesntExist(roadData.Id));
            }

            var result = new Response();
            if (await ValidateModel(roadData, result)) throw result.Exception;

            await _unitOfWork.RoadExpenseRepository.UpdateAsync(roadData);
            await _unitOfWork.SaveChangesAsync();

        });
    }
    public async Task<Response<List<RoadExpense>>> GetAll(long tripId)
    {
        return await RunAsync(async () =>
        {
            return await _unitOfWork.RoadExpenseRepository.WhereAsync(a => a.Trip.Id == tripId && a.IsValid, GenerateIncludes().ToArray());

        });
    }

    public async Task<Response<RoadExpense>> GetById(long id)
    {
        return await RunAsync(async () =>
        {
            return await _unitOfWork.RoadExpenseRepository.FirstOrDefault(a => a.Id == id && a.IsValid, GenerateIncludes().ToArray());

        });
    }
    protected override async Task<bool> ValidateModel(RoadExpense model, Response result)
    {
        if (string.IsNullOrWhiteSpace(model.Name))
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.Name)));
            return true;
        }
        if (model.Quantity == 0)
        {
            result.Exception = new ObiException(ErrorMessages.BiggernThan(nameof(model.Quantity)));
            return true;
        }
        if (model.Price == 0)
        {
            result.Exception = new ObiException(ErrorMessages.BiggernThan(nameof(model.Price)));
            return true;
        }
        if (model.Trip is null || model.Trip.Id == 0)
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.Trip)));
            return true;
        }
        if (model.ExpenseType is null || model.ExpenseType.Id == 0)
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.ExpenseType)));
            return true;
        }
        model.Trip = await _unitOfWork.TripRepository.FirstOrDefault(a => a.Id == model.Trip.Id);
        model.ExpenseType = await _unitOfWork.ExpenseTypeRepository.FirstOrDefault(a => a.Id == model.ExpenseType.Id);

        if (model.Payment is null || model.Payment.Currency == null || model.Payment.Currency.Id == 0)
        {
            result.Exception = new ObiException(ErrorMessages.NotNull("CurrencyId"));
            return true;
        }
        model.Payment.Currency = await _unitOfWork.CurrencyRepository.FirstOrDefault(a => a.Id == model.Payment.Currency.Id);
        if (model.RoadData != null && model.RoadData.Id > 0)
        {
            model.RoadData = await _unitOfWork.RoadDataRepository.FirstOrDefault(a => a.Id == model.RoadData.Id);
        }
        if (model.Country != null && model.Country.Id > 0)
        {
            model.Country = await _unitOfWork.CountryRepository.FirstOrDefault(a => a.Id == model.Country.Id);
        }

        return false;
    }
    private List<Expression<Func<RoadExpense, object>>> GenerateIncludes()
    {
        return new List<Expression<Func<RoadExpense, object>>> {
            a=>a.Trip,
            a=>a.RoadData,
            a=>a.Country,
            a=>a.ExpenseType,
            a=>a.Payment,
            a=>a.Payment.Currency
        };
    }
}
