using DK.Domain.Entity.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Data.EF.Configuration.SecurityConfiguration
{
    class PermissionRoleEntityTypeConfiguration : IBaseEntityTypeConfiguration<PermissionRole>
    {
        public void Configure(EntityTypeBuilder<PermissionRole> builder)
        {
            //Fields
            builder.HasKey(k => new { k.xPermissionID, k.xRoleID });

            //Relation
            builder.HasOne(c => c.xPermission).WithMany(c => c.xPermissionRoles).HasForeignKey(f => f.xPermissionID);
            builder.HasOne(c => c.xRole).WithMany(c => c.xPermissionRoles).HasForeignKey(f => f.xRoleID);

            //Table
            builder.ToTable("PermissionRoles", "Security");

        }
    }
}
