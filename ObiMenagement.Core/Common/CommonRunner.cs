using System.Linq.Expressions;
using Microsoft.Extensions.Logging;

namespace ObiMenagement.Core.Common;

public abstract class CommonRunner
{
    protected readonly ILogger _logger;

    public CommonRunner(ILogger logger)
    {
        _logger = logger;
    }

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
           LogError(result, ex);
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
            LogError(result, ex);
            result.Result = default(T);
        }

        return result;
    }

    #endregion

    #region Run Async

    #region Not implemented on c# yet
    //doesnt work bc aync land cant be converted to expression 
    //public async Task<Response<T>> RunAsync<T>(Expression<Func<Task<T>>> method)
    //{
    //    var result = new Response<T>();
    //    try
    //    {
    //        result.Result = await method.Compile().Invoke();
    //    }

    //    catch (Exception ex)
    //    {
    //        Logger.Instance.LogError(ex);
    //        result.Exception = ex;
    //        result.Result = default(T);
    //    }

    //    return result;
    //}
    //public async Task<Response> RunAsync<T>(Expression<Func<Task>> method)
    //{
    //    var result = new Response();
    //    try
    //    {
    //        await method.Compile().Invoke();
    //    }

    //    catch (Exception ex)
    //    {
    //        Logger.Instance.LogError(ex);
    //        result.Exception = ex;
    //    }

    //    return result;
    //}
    #endregion
    public async Task<Response<T>> RunAsync<T>(Func<Task<T>> method)
    {
        var result = new Response<T>();
        try
        {
            result.Result = await method();
        }

        catch (Exception ex)
        {
            LogError(result, ex);
            result.Result = default(T);
        }

        return result;
    }

    public async Task<Response> RunAsync(Func<Task> method)
    {
        var result = new Response();
        try
        {
            await method();
        }

        catch (Exception ex)
        {
           LogError(result,ex);
        }

        return result;
    }
    #endregion
    private static void LogError(Response result, Exception ex)
    {
        Logger.Instance.LogError(ex);
        result.Exception = ex;
    }
}
