using ObiMenagement.Core.Common;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Interfaces;

public interface IEmployeeService:ICrudService<Employee>
{
    Task<Response<IEnumerable<Employee>>> GetAllWithoutMetadataAsync();
}