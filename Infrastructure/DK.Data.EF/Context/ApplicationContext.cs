using DK.Data.EF.Configuration;
using DK.Domain.Entity.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DK.Data.EF.Context.Extensions;
using DK.Domain.Entity.Base;


namespace DK.Data.EF.Context
{
    public class ApplicationDBContext : IdentityDbContext<User,Role,long,UserClaim,UserRole,UserLogin,RoleClaim,UserToken>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }
        public ApplicationDBContext() : base() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.AddDBSetFromModel(typeof(IEntity).Assembly, typeof(IEntity));
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IBaseEntityTypeConfiguration<>).Assembly);
        }
    }
}
