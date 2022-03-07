using ObiMenagement.Core.Common;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Interfaces;

public interface ICurrencyService:ICrudService<Currency>
{
    Task<Response<IEnumerable<Currency>>> GetAllWithoutMetadataAsync();
}