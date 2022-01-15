using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Interfaces;

public interface IUnitOfWork
{
    IRepository<City> CityRepository { get;  }
    IRepository<County> CountryRepository { get;  }
    Task SaveChangesAsync();
}