using DK.Domain.Entity.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Data.EF.Configuration
{
    class UserRoleEntityTypeConfiguration : IBaseEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            //Fields
            builder.HasKey(r => new { r.UserId, r.RoleId });
            builder.ToTable(nameof(UserRole), "Security").Property(p => p.RoleId).HasColumnName("xRoleID");
            builder.ToTable(nameof(UserRole), "Security").Property(p => p.UserId).HasColumnName("xUserID");

            //Table
            builder.ToTable("UserRoles", "Security");
        }
    }
}
