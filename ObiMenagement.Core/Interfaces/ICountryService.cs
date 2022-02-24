using ObiMenagement.Core.Common;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Interfaces;

public interface ICountryService:ICrudService<Country>
{
    Task<Response<IEnumerable<Country>>> GetAllWithoutMetadataAsync();
}