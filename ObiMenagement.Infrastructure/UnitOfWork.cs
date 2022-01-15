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
    public IRepository<County> CountryRepository =>new Repository<County>(_context);

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}