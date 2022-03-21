using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Infrastructure.Configurations;

public class ExpenseTypeEntityTypeConfiguration : IEntityTypeConfiguration<ExpenseType>
{
    public void Configure(EntityTypeBuilder<ExpenseType> builder)
    {
        builder
            .Property(b => b.Name).HasMaxLength(200)
            .IsRequired();
        builder.OwnsOne(b => b.DefaultPayment);
    }
}
