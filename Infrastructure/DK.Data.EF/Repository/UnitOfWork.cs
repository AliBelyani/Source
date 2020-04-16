using AutoMapper;
using DK.Data.EF.Context;
using DK.Domain.Entity.Security;
using DK.Service.Interface.Repository;
using System;
using System.Threading.Tasks;

namespace DK.Data.EF.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Initial Repository
        private readonly ApplicationDBContext _context;

        private IGenericRepository<User> _UserRepository;
        private IGenericRepository<Role> _RoleRepository;
        private IGenericRepository<Permission> _PermissionRepository;
        private IGenericRepository<PermissionGroup> _PermissionGroupRepository;
        private IGenericRepository<UserLoginHistory> _UserLoginHistoryRepository;

        #endregion

        #region Ctor
        IMapper _mapper;
        public UnitOfWork(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region Security
        public IGenericRepository<User> UserRepository
        {
            get
            {
                return _UserRepository ?? (_UserRepository = new GenericRepository<User>(_context, _mapper));
            }
        }
        public IGenericRepository<Role> RoleRepository
        {
            get
            {
                return _RoleRepository ?? (_RoleRepository = new GenericRepository<Role>(_context, _mapper));
            }
        }
        public IGenericRepository<Permission> PermissionRepository
        {
            get
            {
                return _PermissionRepository ?? (_PermissionRepository = new GenericRepository<Permission>(_context, _mapper));
            }
        }
        public IGenericRepository<PermissionGroup> PermissionGroupRepository
        {
            get
            {
                return _PermissionGroupRepository ?? (_PermissionGroupRepository = new GenericRepository<PermissionGroup>(_context, _mapper));
            }
        }
        public IGenericRepository<UserLoginHistory> UserLoginHistoryRepository
        {
            get
            {
                return _UserLoginHistoryRepository ?? (_UserLoginHistoryRepository = new GenericRepository<UserLoginHistory>(_context, _mapper));
            }
        }
        #endregion

        #region Base
        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
