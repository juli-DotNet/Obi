using System.Linq.Expressions;
using ObiMenagement.Core.Common;

namespace ObiMenagement.Core.Services;

public class BaseService
{
    #region Run
    public Response Run(Expression<Action> method)
    {
        var result = new Response();
        try
        {
            method.Compile().Invoke();
            result.IsSuccessful = true;
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
            result.IsSuccessful = true;
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

    
    
    protected async  Task<Response<T>> RunAsync<T>(Task<Expression<Func<Task<T>>>> method)
    {
        var result = new Response<T>();
        try
        {
            result.Result =await (await method).Compile().Invoke();
            result.IsSuccessful = true;
        }

        catch (Exception ex)
        {
            Logger.Instance.LogError(ex);
            result.Exception = ex;
            result.Result = default(T);
        }

        return result;
    }
    protected async  Task<Response> RunAsync(Task<Action> method)
    {
        var result = new Response();
        try
        {
            // var methodCallExp = (MethodCallExpression)method.Body;
            // string methodName = methodCallExp.Method.Name;
            // Type type = methodCallExp.Method.DeclaringType;
            await method;
            result.IsSuccessful = true;
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