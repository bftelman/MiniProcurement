﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);

        builder
            .Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(128);

        builder.HasMany(r => r.Users)
            .WithMany(r => r.Roles);
    }
}