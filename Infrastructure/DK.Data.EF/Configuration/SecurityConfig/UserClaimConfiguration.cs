using DK.Domain.Entity.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Data.EF.Configuration
{
    public class UserClaimEntityTypeConfiguration : IBaseEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            //Fields
            builder.HasKey(l => new { l.Id });
            builder.ToTable(nameof(UserClaim), "Security").Property(p => p.Id).HasColumnName("xID");
            builder.ToTable(nameof(UserClaim), "Security").Property(p => p.ClaimType).HasColumnName("xClaimType");
            builder.ToTable(nameof(UserClaim), "Security").Property(p => p.ClaimValue).HasColumnName("xClaimValue");
            builder.ToTable(nameof(UserClaim), "Security").Property(p => p.UserId).HasColumnName("xUserID");

            //Table
            builder.ToTable("UserClaims", "Security");
        }
    }
}
