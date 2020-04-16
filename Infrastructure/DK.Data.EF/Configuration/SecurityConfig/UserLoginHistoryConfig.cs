using DK.Domain.Entity.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DK.Data.EF.Configuration.SecurityConfig
{
    public class UserLoginHistoryConfig : IBaseEntityTypeConfiguration<UserLoginHistory>
    {
        public void Configure(EntityTypeBuilder<UserLoginHistory> builder)
        {
            //Fields
            builder.HasKey(k => k.xID);

            //Navigation
            builder.HasOne(i => i.xUser).WithMany(p => p.xUserLoginHistories).HasForeignKey(i => i.xUserId);

            //Table
            builder.ToTable("UserLoginHistories", "Security");
        }
    }
}
