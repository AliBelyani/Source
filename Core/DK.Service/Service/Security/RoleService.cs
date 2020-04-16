using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using DK.Domain.DTO.Base;
using DK.Domain.Entity.Security;
using DK.Service.Interface.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using DK.Domain.DTO.Security.Roles;

namespace DK.Service.Service.Security
{
    public interface IRoleService
    {
        Task<TViewModel> GetByID<TViewModel>(long xID);
        Task<PagingResult<TViewModel>> GetsAsync<TViewModel>(bool NeedDBCount, RoleSearchParameter xParameters = null);
        Task<List<GeneralSelectListItem>> GetAll();
        Task<List<string>> AddAsync(RoleVM model);
        Task<List<string>> DeleteAsync(long xID);
        Task<List<string>> EditAsync(RoleVM model);


    }
    public class RoleService : BaseService, IRoleService
    {
        #region == Ctor ==
        private readonly UserManager<User> _UserManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<User> _SignInManager;

        public RoleService(IUnitOfWork uow, IMapper IMapper) : base(uow, IMapper)
        { }
        #endregion

        #region   == Read Data ==
        public async Task<TViewModel> GetByID<TViewModel>(long xID)
        {
            return await uow.RoleRepository.GetFirstAsync<TViewModel>(x => x.Id == xID);
        }

        public async Task<PagingResult<TViewModel>> GetsAsync<TViewModel>(bool NeedDBCount, RoleSearchParameter xParameters = null)
        {
            if (xParameters == null)
                xParameters = new RoleSearchParameter();
            #region OrderBy
            Func<IQueryable<Role>, IOrderedQueryable<Role>> xOrderBy = p => p.OrderByDescending(x => x.Id);
            if (xParameters.xSortBy == "xName")
            {
                if (xParameters.xSortType == "Des")
                {
                    xOrderBy = p => p.OrderByDescending(x => x.Name);
                }
                else
                {
                    xOrderBy = p => p.OrderBy(x => x.Name);
                }
            }
            else if (xParameters.xSortBy == "xDescription")
            {
                if (xParameters.xSortType == "Des")
                {
                    xOrderBy = p => p.OrderByDescending(x => x.xDescription);
                }
                else
                {
                    xOrderBy = p => p.OrderBy(x => x.xDescription);
                }
            }

            #endregion

            #region Filter
            Expression<Func<Role, bool>> xFilter = p =>
                        (xParameters.xName == "" || xParameters.xName == null || p.Name.Trim().ToLower().Contains(xParameters.xName.Trim().ToLower()));


            #endregion

            return await uow.RoleRepository.GetAsync<TViewModel>(NeedDBCount, xFilter, xOrderBy, xParameters.xPage, xParameters.xPageSize);
        }
        #endregion

        #region == Post Data ==
        public async Task<List<string>> AddAsync(RoleVM model)
        {
            List<string> xErrors = new List<string>();

            var xDbModel = new Role
            {
                Name = model.xName,
                xDescription = model.xDescription

            };

            await uow.RoleRepository.InsertAsync(xDbModel);
            await uow.SaveChangesAsync();
            return xErrors;
        }
        #endregion

        #region == Put Data ==
        public async Task<List<string>> EditAsync(RoleVM model)
        {
            if (model.xID == 0)
                throw new ArgumentNullException("no identifier provided");
            var xDBModel = uow.RoleRepository.GetByID(model.xID);
            if (xDBModel != null)
            {
                xDBModel.Name = model.xName;
                xDBModel.xDescription = model.xDescription;
                uow.RoleRepository.Update(xDBModel);
                await uow.SaveChangesAsync();
                return new List<string>();
            }
            return new List<string> { "آیتم مورد نظر در سیستم موجود نمیباشد" };
        }
        #endregion

        #region == Delete Data ==
        public async Task<List<string>> DeleteAsync(long xID)
        {
            if (uow.PermissionRepository.Any(x => x.xPermissionRoles.Any(a => a.xRole.Id == xID)))
                return new List<string> { "حذف نقش دارای مجوز امکان پذیر نمیباشد" };

            await uow.RoleRepository.DeleteAsync(xID);
            await uow.SaveChangesAsync();
            return new List<string>();
        }

        public async Task<List<GeneralSelectListItem>> GetAll()
        {
            var result = await uow.RoleRepository.GetAsync();

            var selectionList = new List<GeneralSelectListItem>();

            foreach (var role in result)
            {
                selectionList.Add(new GeneralSelectListItem(){
                    xText = role.Name,
                    xValue = role.Id.ToString()
                });
            }

            return selectionList; 
        }
        #endregion
    }
}
