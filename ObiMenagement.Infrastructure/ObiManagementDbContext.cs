using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Infrastructure;

public class ObiManagementDbContext:IdentityDbContext
{
    public ObiManagementDbContext(DbContextOptions<ObiManagementDbContext> options)
        : base(options)
    {
        
    }
    public DbSet<City> City { get; set; }
}