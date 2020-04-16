using DK.Domain.Entity.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Data.EF.Configuration.SecurityConfiguration
{
    class PermissionEntityTypeConfiguration : IBaseEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            //Fields
            builder.HasKey(k => k.xID);

            //Relation
            builder.HasOne(r => r.xPermissionGroup).WithMany(r => r.xPermissions).HasForeignKey(r => r.xPermissionGroupID);

            //Table
            builder.ToTable("Permissions", "Security");
        }
    }
}
