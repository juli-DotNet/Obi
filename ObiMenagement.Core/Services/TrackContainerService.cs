using ObiMenagement.Core.Common;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Services;

public class TrackContainerService : BaseService, ITrackContainerService
{
    private readonly IUnitOfWork _unitOfWork;

    public TrackContainerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    private async Task<bool> ValidateModel(TruckContainer model, Response result)
    {
        if (string.IsNullOrWhiteSpace(model.Plate))
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.Plate)));
            return true;
        }

        return false;
    }

    public async Task<Response> CreateAsync(TruckContainer model)
    {
        var result = new Response();
        try
        {
            if (await ValidateModel(model, result)) return result;

            await _unitOfWork.TruckContainerRepository.InsertAsync(model);
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
            if (!await _unitOfWork.TruckContainerRepository.AnyAsync(a => a.Id ==id && a.IsValid))
            {
                result.Exception = new ObiException(ErrorMessages.EntityDoesntExist(id));
                return result;
            }

            await _unitOfWork.TruckContainerRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Logger.Instance.LogError(e);
            result.Exception = e;
        }

        return result;
    }

    public async Task<Response> EditAsync(TruckContainer model)
    {
        var result = new Response();
        try
        {
            if (!await _unitOfWork.TruckContainerRepository.AnyAsync(a => a.Id ==model.Id && a.IsValid))
            {
                result.Exception = new ObiException(ErrorMessages.EntityDoesntExist(model.Id));
                return result;
            }
            if (await ValidateModel(model, result)) return result;

            await _unitOfWork.TruckContainerRepository.UpdateAsync(model);
            await _unitOfWork.SaveChangesAsync();

        }
        catch (Exception e)
        {
            Logger.Instance.LogError(e);
            result.Exception = e;
        }

        return result;
    }

    public async Task<Response<IEnumerable<TruckContainer>>> GetAllAsync()
    {
        var result = new Response<IEnumerable<TruckContainer>>();

        try
        {
            result.Result = await _unitOfWork.TruckContainerRepository.WhereAsync(a => true);
        }
        catch (Exception e)
        {
            result.Exception = e;
            Logger.Instance.LogError(e);
        }
        return result;
    }

    public async Task<Response<TruckContainer>> GetByIdAsync(int id)
    {
        var result = new Response<TruckContainer>();

        try
        {
            result.Result = await _unitOfWork.TruckContainerRepository.FirstOrDefault(a => a.Id==id);
        }
        catch (Exception e)
        {
            result.Exception = e;
            Logger.Instance.LogError(e);
        }
        return result;
    }
}