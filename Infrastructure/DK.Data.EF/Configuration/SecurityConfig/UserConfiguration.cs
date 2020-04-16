using DK.Domain.Entity.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Data.EF.Configuration.SecurityConfiguration
{
    public class UserEntityTypeConfiguration: IBaseEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            
            //Fields
            builder.HasKey(l => new { l.Id });
            builder.Property(p => p.Id).HasColumnName("xID");
            builder.Property(p => p.PasswordHash).HasColumnName("xPasswordHash");
            builder.Property(p => p.SecurityStamp).HasColumnName("xSecurityStamp");
            builder.Property(p => p.UserName).HasColumnName("xUsername");
            builder.Property(p => p.Email).HasColumnName("xEmail");
            builder.Property(p => p.EmailConfirmed).HasColumnName("xEmailConfirmed");
            //builder.Ignore(p => p.PhoneNumber);
            //builder.Ignore(p => p.PhoneNumberConfirmed);
            builder.Property(p => p.PhoneNumber).HasColumnName("xMobile").HasMaxLength(20);
            builder.Property(p => p.PhoneNumberConfirmed).HasColumnName("xMobileConfirmed");
            builder.Property(p => p.TwoFactorEnabled).HasColumnName("xTwoFactorEnabled");
            builder.Property(p => p.LockoutEnd).HasColumnName("xLockoutEnd");
            builder.Property(p => p.LockoutEnabled).HasColumnName("xLockoutEnabled");
            builder.Property(p => p.AccessFailedCount).HasColumnName("xAccessFailedCount");
            //builder.Ignore(u => u.Claims);
            //builder.Ignore(u => u.Logins);

            //Navigation
            builder.ToTable("Users","Security").HasMany(u => u.xUserRoles).WithOne(ur => ur.xUser)
                 .HasForeignKey(ur => ur.UserId)
                 .IsRequired();
          

            //Table
            //builder.ToTable("Users", "Security");
        }
    }
}
