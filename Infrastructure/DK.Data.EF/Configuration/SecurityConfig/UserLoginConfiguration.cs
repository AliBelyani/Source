using DK.Domain.Entity.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Data.EF.Configuration.SecurityConfiguration
{
    class UserLoginEntityTypeConfiguration : IBaseEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            //Fields
            builder.HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId });
            builder.ToTable(nameof(UserLogin), "Security").Property(p => p.UserId).HasColumnName("xUserID");
            builder.ToTable(nameof(UserLogin), "Security").Property(p => p.LoginProvider).HasColumnName("xLoginProvider");
            builder.ToTable(nameof(UserLogin), "Security").Property(p => p.ProviderKey).HasColumnName("xProviderKey");

            //Table
            builder.ToTable("UserLogins", "Security");
        }
    }
}
