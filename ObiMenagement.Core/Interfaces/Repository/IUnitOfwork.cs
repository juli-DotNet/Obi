using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Interfaces;

public interface IUnitOfWork
{
    IRepository<City> CityRepository { get;  }
    IRepository<Country> CountryRepository { get;  }
    IRepository<Currency> CurrencyRepository { get;  }
    IRepository<TruckBase> TruckBaseRepository { get;  }
    IRepository<TruckContainer> TruckContainerRepository { get;  }
    IRepository<Location> LocationRepository { get;  }
    IRepository<Person> PersonRepository { get;  }
    IRepository<Employee> EmployeeRepository { get;  }
    IRepository<ExpenseType> ExpenseTypeRepository { get; }
    IRepository<Client> ClientRepository { get; }
    IRepository<RoadClient> RoadClientRepository { get; }
    IRepository<ClientContact> ClientContactRepository { get; }
    Task SaveChangesAsync();
}