using ObiMenagement.Core.Common;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Interfaces;

public interface ILocationService:ICrudService<Location>
{
    Task<Response<IEnumerable<Location>>> GetAllWithoutMetadataAsync();
}