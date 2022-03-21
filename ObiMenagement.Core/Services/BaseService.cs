using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using ObiMenagement.Core.Common;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Services;

public abstract class BaseService<T> where T : IdBaseModel
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IRepository<T> _repository;
    protected readonly ILogger _logger;

    public BaseService(IUnitOfWork unitOfWork,IRepository<T> repository,ILogger logger)
    {
        this._unitOfWork = unitOfWork;
        _repository = repository;
        this._logger = logger;
    }
    protected abstract Task<bool> ValidateModel(T model, Response result);

    #region CRUD
    public virtual async Task<Response> CreateAsync(T model)
    {
        var result = new Response();
        try
        {
            if (await ValidateModel(model, result)) return result;

            await _repository.InsertAsync(model);
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

    public virtual async Task<Response> DeleteAsync(int id)
    {
        var result = new Response();

        try
        {
            if (!await _repository.AnyAsync(a => a.Id == id && a.IsValid))
            {
                result.Exception = new ObiException(ErrorMessages.EntityDoesntExist(id));
                return result;
            }

            await _repository.DeleteAsync(id);
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

    public virtual async Task<Response> EditAsync(T model)
    {
        var result = new Response();
        try
        {
            if (!await _repository.AnyAsync(a => a.Id == model.Id && a.IsValid))
            {
                result.Exception = new ObiException(ErrorMessages.EntityDoesntExist(model.Id));
                return result;
            }
            if (await ValidateModel(model, result)) return result;

            await _repository.UpdateAsync(model);
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

    public virtual async Task<Response<IEnumerable<T>>> GetAllAsync(string search = null)
    {
        var result = new Response<IEnumerable<T>>();

        try
        {
            result.Result = await _repository.WhereAsync(a => true);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "failed to delete the model");
            result.Exception = e;
            Logger.Instance.LogError(e);
        }
        return result;
    }

    public virtual async Task<Response<T>> GetByIdAsync(int id)
    {
        var result = new Response<T>();

        try
        {
            result.Result = await _repository.FirstOrDefault(a => a.Id == id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "failed to delete the model");
            result.Exception = e;
            Logger.Instance.LogError(e);
        }
        return result;
    }

    #endregion



    #region Run
    public Response Run(Expression<Action> method)
    {
        var result = new Response();
        try
        {
            method.Compile().Invoke();
        }

        catch (Exception ex)
        {
            Logger.Instance.LogError(ex);
            result.Exception = ex;
        }

        return result;
    }
    public  Response<T> Run<T>(Expression<Func<T>> method)
    {
        var result = new Response<T>();
        try
        {
            result.Result= method.Compile().Invoke();
        }

        catch (Exception ex)
        {
            Logger.Instance.LogError(ex);
            result.Exception = ex;
            result.Result = default(T);
        }

        return result;
    }
    

    #endregion

    #region Run Async

    
    
    // protected async  Task<Response<T>> RunAsync<T>(Task<Expression<Func<Task<T>>>> method)
    // {
    //     var result = new Response<T>();
    //     try
    //     {
    //         result.Result =await (await method).Compile().Invoke();
    //         result.IsSuccessful = true;
    //     }
    //
    //     catch (Exception ex)
    //     {
    //         Logger.Instance.LogError(ex);
    //         result.Exception = ex;
    //         result.Result = default(T);
    //     }
    //
    //     return result;
    // }
    public   Response RunAsync(Task<Action> method)
    {
        var result = new Response();
        try
        {
            // var methodCallExp = (MethodCallExpression)method.Body;
            // string methodName = methodCallExp.Method.Name;
            // Type type = methodCallExp.Method.DeclaringType;
            method.GetAwaiter().GetResult();
        }
    
        catch (Exception ex)
        {
            Logger.Instance.LogError(ex);
            result.Exception = ex;
        }
    
        return result;
    }

    #endregion
   
  
}

public abstract class LongBaseService<T> where T : IdLongBaseModel
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IRepository<T> _repository;
    protected readonly ILogger _logger;

    public LongBaseService(IUnitOfWork unitOfWork, IRepository<T> repository, ILogger logger)
    {
        this._unitOfWork = unitOfWork;
        _repository = repository;
        this._logger = logger;
    }
    protected abstract Task<bool> ValidateModel(T model, Response result);

    #region CRUD
    public virtual async Task<Response> CreateAsync(T model)
    {
        var result = new Response();
        try
        {
            if (await ValidateModel(model, result)) return result;

            await _repository.InsertAsync(model);
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

    public virtual async Task<Response> DeleteAsync(int id)
    {
        var result = new Response();

        try
        {
            if (!await _repository.AnyAsync(a => a.Id == id && a.IsValid))
            {
                result.Exception = new ObiException(ErrorMessages.EntityDoesntExist(id));
                return result;
            }

            await _repository.DeleteAsync(id);
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

    public virtual async Task<Response> EditAsync(T model)
    {
        var result = new Response();
        try
        {
            if (!await _repository.AnyAsync(a => a.Id == model.Id && a.IsValid))
            {
                result.Exception = new ObiException(ErrorMessages.EntityDoesntExist(model.Id));
                return result;
            }
            if (await ValidateModel(model, result)) return result;

            await _repository.UpdateAsync(model);
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

    public virtual async Task<Response<IEnumerable<T>>> GetAllAsync(string search = null)
    {
        var result = new Response<IEnumerable<T>>();

        try
        {
            result.Result = await _repository.WhereAsync(a => true);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "failed to delete the model");
            result.Exception = e;
            Logger.Instance.LogError(e);
        }
        return result;
    }

    public virtual async Task<Response<T>> GetByIdAsync(int id)
    {
        var result = new Response<T>();

        try
        {
            result.Result = await _repository.FirstOrDefault(a => a.Id == id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "failed to delete the model");
            result.Exception = e;
            Logger.Instance.LogError(e);
        }
        return result;
    }

    #endregion



    #region Run
    public Response Run(Expression<Action> method)
    {
        var result = new Response();
        try
        {
            method.Compile().Invoke();
        }

        catch (Exception ex)
        {
            Logger.Instance.LogError(ex);
            result.Exception = ex;
        }

        return result;
    }
    public Response<T> Run<T>(Expression<Func<T>> method)
    {
        var result = new Response<T>();
        try
        {
            result.Result = method.Compile().Invoke();
        }

        catch (Exception ex)
        {
            Logger.Instance.LogError(ex);
            result.Exception = ex;
            result.Result = default(T);
        }

        return result;
    }


    #endregion

    #region Run Async



    // protected async  Task<Response<T>> RunAsync<T>(Task<Expression<Func<Task<T>>>> method)
    // {
    //     var result = new Response<T>();
    //     try
    //     {
    //         result.Result =await (await method).Compile().Invoke();
    //         result.IsSuccessful = true;
    //     }
    //
    //     catch (Exception ex)
    //     {
    //         Logger.Instance.LogError(ex);
    //         result.Exception = ex;
    //         result.Result = default(T);
    //     }
    //
    //     return result;
    // }
    public Response RunAsync(Task<Action> method)
    {
        var result = new Response();
        try
        {
            // var methodCallExp = (MethodCallExpression)method.Body;
            // string methodName = methodCallExp.Method.Name;
            // Type type = methodCallExp.Method.DeclaringType;
            method.GetAwaiter().GetResult();
        }

        catch (Exception ex)
        {
            Logger.Instance.LogError(ex);
            result.Exception = ex;
        }

        return result;
    }

    #endregion


}
