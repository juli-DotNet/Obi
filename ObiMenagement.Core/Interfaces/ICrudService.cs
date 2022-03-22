using ObiMenagement.Core.Common;

namespace ObiMenagement.Core.Interfaces;

public interface ICrudService<T>
{
    Task<Response> CreateAsync(T model);
    Task<Response> DeleteAsync(int id);
    Task<Response> EditAsync(T model);
    Task<Response<IEnumerable<T>>> GetAllAsync(string search = null);
    Task<Response<T>> GetByIdAsync(int id);
}