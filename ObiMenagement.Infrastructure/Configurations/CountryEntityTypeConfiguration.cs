using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Infrastructure.Configurations;

public class CountryEntityTypeConfiguration: IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder
            .Property(b => b.Name).HasMaxLength(200)
            .IsRequired();
    }
}