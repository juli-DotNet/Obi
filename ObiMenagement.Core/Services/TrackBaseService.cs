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
    public TrackBaseService(IUnitOfWork unitOfWork,ILogger<TrackBaseService> logger):base(unitOfWork,unitOfWork.TruckBaseRepository,logger)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response> CreateAsync(TruckBase model)
    {
        var result = new Response();
        try
        {
            if (await ValidateModel(model, result)) return result;

            await _unitOfWork.TruckBaseRepository.InsertAsync(model);
            await _unitOfWork.SaveChangesAsync();

        }
        catch (Exception e)
        {
            Logger.Instance.LogError(e);
            result.Exception = e;
        }

        return result;
    }

    public async Task<Response> DeleteAsync(int id)
    {
        var result = new Response();

        try
        {
            if (!await _unitOfWork.TruckBaseRepository.AnyAsync(a => a.Id ==id && a.IsValid))
            {
                result.Exception = new ObiException(ErrorMessages.EntityDoesntExist(id));
                return result;
            }

            await _unitOfWork.TruckBaseRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Logger.Instance.LogError(e);
            result.Exception = e;
        }

        return result;
    }

    public async Task<Response> EditAsync(TruckBase model)
    {
        var result = new Response();
        try
        {
            if (!await _unitOfWork.TruckBaseRepository.AnyAsync(a => a.Id ==model.Id && a.IsValid))
            {
                result.Exception = new ObiException(ErrorMessages.EntityDoesntExist(model.Id));
                return result;
            }
            if (await ValidateModel(model, result)) return result;

            await _unitOfWork.TruckBaseRepository.UpdateAsync(model);
            await _unitOfWork.SaveChangesAsync();

        }
        catch (Exception e)
        {
            Logger.Instance.LogError(e);
            result.Exception = e;
        }

        return result;
    }

    public async Task<Response<IEnumerable<TruckBase>>> GetAllAsync()
    {
        var result = new Response<IEnumerable<TruckBase>>();

        try
        {
            result.Result = await _unitOfWork.TruckBaseRepository.WhereAsync(a => true);
        }
        catch (Exception e)
        {
            result.Exception = e;
            Logger.Instance.LogError(e);
        }
        return result;
    }

    public async Task<Response<TruckBase>> GetByIdAsync(int id)
    {
        var result = new Response<TruckBase>();

        try
        {
            result.Result = await _unitOfWork.TruckBaseRepository.FirstOrDefault(a => a.Id==id);
        }
        catch (Exception e)
        {
            result.Exception = e;
            Logger.Instance.LogError(e);
        }
        return result;
    }
}