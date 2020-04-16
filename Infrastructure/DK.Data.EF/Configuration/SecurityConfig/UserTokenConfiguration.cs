using DK.Domain.Entity.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Data.EF.Configuration
{
    public class UserTokenEntityTypeConfiguration : IBaseEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {

            //Table
            builder.ToTable("UserTokens", "Security");
        }
    }
}
