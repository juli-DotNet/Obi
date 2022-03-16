using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ObiManagementDbContext _context;

    public UnitOfWork(ObiManagementDbContext context)
    {
        _context = context;
    }

    public IRepository<City> CityRepository =>new Repository<City>(_context);
    public IRepository<Country> CountryRepository =>new Repository<Country>(_context);
    public IRepository<Currency> CurrencyRepository =>new Repository<Currency>(_context);
    public IRepository<TruckBase> TruckBaseRepository =>new Repository<TruckBase>(_context);
    public IRepository<TruckContainer> TruckContainerRepository =>new Repository<TruckContainer>(_context);
    public IRepository<Location> LocationRepository =>new Repository<Location>(_context);
    public IRepository<Person> PersonRepository =>new Repository<Person>(_context);
    public IRepository<Employee> EmployeeRepository =>new Repository<Employee>(_context);
    public IRepository<ExpenseType> ExpenseTypeRepository =>new Repository<ExpenseType>(_context);
    public IRepository<Client> ClientRepository =>new Repository<Client>(_context);
    public IRepository<RoadClient> RoadClientRepository =>new Repository<RoadClient>(_context);

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}