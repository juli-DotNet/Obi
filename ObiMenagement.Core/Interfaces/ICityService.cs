using ObiMenagement.Core.Common;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Interfaces;

public interface ICityService:ICrudService<City>
{
    Task<Response<IEnumerable<City>>> GetAllWithoutMetadataAsync();
}