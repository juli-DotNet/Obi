using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ObiMenagement.Core.Models;
using ObiMenagement.Infrastructure.Configurations;

namespace ObiMenagement.Infrastructure;

public class ObiManagementDbContext:IdentityDbContext
{
    public ObiManagementDbContext(DbContextOptions<ObiManagementDbContext> options)
        : base(options)
    {
        
    }
    public DbSet<City> City { get; set; }
    public DbSet<Country> Country { get; set; }
    public DbSet<Currency> Currency { get; set; }
    public DbSet<Employee> Employee { get; set; }
    public DbSet<Person> Person { get; set; }
    public DbSet<TruckBase> TruckBase { get; set; }
    public DbSet<TruckContainer> TruckContainer { get; set; }
    public DbSet<Location> Location { get; set; }
    public DbSet<Client> Client { get; set; }
    public DbSet<ClientContact> ClientContact { get; set; }
    public DbSet<RoadClient> RoadClient { get; set; }
    public DbSet<ExpenseType> ExpenseType { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CountryEntityTypeConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    
}