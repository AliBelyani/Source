using DK.Domain.Entity.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DK.Data.EF.Configuration.SecurityConfiguration
{
    class PermissionGroupEntityTypeConfiguration : IBaseEntityTypeConfiguration<PermissionGroup>
    {
        public void Configure(EntityTypeBuilder<PermissionGroup> builder)
        {
            //Fields
            builder.HasKey(k => k.xID);
            builder.Property(p => p.xName).HasMaxLength(100);
            builder.HasMany(i => i.xPermissionGroups).WithOne(p => p.xPermissionGroup).HasForeignKey(i => i.xParentID);

            //Table
            builder.ToTable("PermissionGroups", "Security");
        }
    }
}
