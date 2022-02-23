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
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CountryEntityTypeConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    
}