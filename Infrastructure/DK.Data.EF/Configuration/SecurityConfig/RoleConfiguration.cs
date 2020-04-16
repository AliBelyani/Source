using DK.Domain.Entity.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Data.EF.Configuration
{
    public class RoleEntityTypeConfiguration : IBaseEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            //Fields
            builder.HasKey(l => new { l.Id });
            builder.ToTable(nameof(Role), "Security").Property(p => p.Id).HasColumnName("xID");
            builder.ToTable(nameof(Role), "Security").Property(p => p.Name).HasColumnName("xName");

            builder.ToTable("Roles").HasKey(r => r.Id);
            builder.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();
            builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(u => u.Name).HasMaxLength(256);
            builder.Property(u => u.NormalizedName).HasMaxLength(256);
            builder.Property(u => u.xDescription).HasMaxLength(100);


            //navigation
            builder.HasMany<UserRole>().WithOne(ur => ur.xRole)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
            builder.HasMany<RoleClaim>().WithOne()
                .HasForeignKey(rc => rc.RoleId)
                .IsRequired();
                                          
            //Table
            builder.ToTable(nameof(Role), "Security");
        }
    }
}
