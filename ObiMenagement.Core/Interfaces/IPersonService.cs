using ObiMenagement.Core.Common;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Interfaces;

public interface IPersonService:ICrudService<Person>
{
    Task<Response<IEnumerable<Person>>> GetAllWithoutMetadataAsync();
}
