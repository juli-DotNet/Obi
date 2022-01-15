using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Interfaces;

public interface IUnitOfWork
{
    IRepository<City> CityRepository { get;  }
    Task SaveChangesAsync();
}