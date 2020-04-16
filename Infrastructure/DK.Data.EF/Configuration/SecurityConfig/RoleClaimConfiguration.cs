using DK.Domain.Entity.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Data.EF.Configuration
{
    public class RoleClaimEntityTypeConfiguration : IBaseEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {

            builder.HasKey(rc => rc.Id);

            //Table
            builder.ToTable("RoleClaims", "Security");
        }
    }
}
