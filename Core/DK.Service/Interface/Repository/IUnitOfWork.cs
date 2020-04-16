using DK.Domain.Entity.Security;
using System;
using System.Threading.Tasks;

namespace DK.Service.Interface.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        #region Security
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Role> RoleRepository { get; }
        IGenericRepository<PermissionGroup> PermissionGroupRepository { get; }
        IGenericRepository<Permission> PermissionRepository { get; }
        IGenericRepository<UserLoginHistory> UserLoginHistoryRepository { get; }
        #endregion

        #region Base
        void SaveChanges();
        Task SaveChangesAsync(); 
        #endregion
    }
}
