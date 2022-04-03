﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Infrastructure.Configurations;

public class RoadExpenseEntityTypeConfiguration : IEntityTypeConfiguration<RoadExpense>
{
    public void Configure(EntityTypeBuilder<RoadExpense> builder)
    {
        builder
            .Property(b => b.Name).HasMaxLength(200)
            .IsRequired();
        builder.OwnsOne(b => b.Payment);
    }
}
