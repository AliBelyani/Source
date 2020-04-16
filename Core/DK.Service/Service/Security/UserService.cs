using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using DK.Domain.DTO.Security.Users;
using DK.Domain.DTO.Base;
using DK.Domain.Entity.Security;
using DK.Service.Interface.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text.RegularExpressions;
using DK.Utility;

namespace DK.Service.Service.Security
{
    public interface IUserService
    {
        Task<PagingResult<TViewModel>> GetsAsync<TViewModel>(bool NeedDBCount, UserSearchParameter xParameters = null);
        Task<List<TViewModel>> GetsSimpleAsync<TViewModel>();
        SimpleUserListVM GetsUserReportAsync(UserReportParam userReportParam);
        Task<TViewModel> Get<TViewModel>(long xID);
        Task<List<string>> AddAsync(UserRegisterVm model);
        Task<bool> EditAsync(UserRegisterVm model);
        Task<bool> EditPasswordAsync(UserEditPasswordVM model);
        Task<bool> DeleteAsync(long[] xID);
        Task<bool> DeleteAsync(long xID);
        Task<UserSummaryVM> GetCurrentUserAsync();
        long GetCurrentUserID();
        long GetCurrentPersonID();
        bool IsLogin();
        bool CheckPermission(string xControllerName, string xActionName, long? xUserID = null);
        Task LogOutAsync();
        Task AddUserHistory();

    }
    public class UserService : BaseService, IUserService
    {
        #region == Ctor ==
        private readonly UserManager<User> _UserManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<User> _SignInManager;

        public UserService(IUnitOfWork uow, IMapper IMapper, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, SignInManager<User> signInManager) : base(uow, IMapper)
        {
            _UserManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _SignInManager = signInManager;
        }
        #endregion

        #region   == Read Data ==
        public async Task<PagingResult<TViewModel>> GetsAsync<TViewModel>(bool NeedDBCount, UserSearchParameter xParameters = null)
        {
            if (xParameters == null)
                xParameters = new UserSearchParameter();
            #region OrderBy
            Func<IQueryable<User>, IOrderedQueryable<User>> xOrderBy = p => p.OrderByDescending(x => x.Id);
            if (xParameters.xSortBy == "Date")
            {
                if (xParameters.xSortType == "Des")
                {
                    xOrderBy = p => p.OrderByDescending(x => x.Id);
                }
                else
                {
                    xOrderBy = p => p.OrderBy(x => x.Id);
                }
            }
            else if (xParameters.xSortBy == "xUsername")
            {
                if (xParameters.xSortType == "Des")
                {
                    xOrderBy = p => p.OrderByDescending(x => x.UserName);
                }
                else
                {
                    xOrderBy = p => p.OrderBy(x => x.UserName);
                }
            }
            else if (xParameters.xSortBy == "xEmail")
            {
                if (xParameters.xSortType == "Des")
                {
                    xOrderBy = p => p.OrderByDescending(x => x.Email);
                }
                else
                {
                    xOrderBy = p => p.OrderBy(x => x.Email);
                }
            }
            #endregion

            #region Filter
            Expression<Func<User, bool>> xFilter = p => p.xIsActive;
            if (!string.IsNullOrEmpty(xParameters.xUsername))
            {
                xFilter = xFilter.And(x => x.UserName.Contains(xParameters.xUsername));
            }
            if (xParameters.xIsActive != null)
            {
                xFilter = xFilter.And(x => x.xIsActive == xParameters.xIsActive);
            }

            #endregion

            return await uow.UserRepository.GetAsync<TViewModel>(NeedDBCount, xFilter, xOrderBy, xParameters.xPage, xParameters.xPageSize);
        }

        public async Task<List<TViewModel>> GetsSimpleAsync<TViewModel>()
        {
            return await uow.UserRepository.GetAsync<TViewModel>(i => i.xIsActive);
        }

        public SimpleUserListVM GetsUserReportAsync(UserReportParam userReportParam = null)
        {
            if (userReportParam == null)
                userReportParam = null;

            DateTime? xStartTime = null;
            DateTime? xEndTime = null;
            if (userReportParam.StartTime.HasValue)
                xStartTime = userReportParam.StartTime.Value.AddHours(3).AddMinutes(30);
            if (userReportParam.EndTime.HasValue)
                xEndTime = userReportParam.EndTime.Value.AddHours(3).AddMinutes(30);

            var currentUserId = GetCurrentUserID();
            var user = new User();
            user = uow.UserRepository.Get(i => i.xIsActive && i.Id == currentUserId, null, "xUserLoginHistories,xUserRoles,xUserRoles.xRole").FirstOrDefault();
            var isAdmin = ((user.xUserRoles != null) && (user.xUserRoles.Any(i => i.xRole.xIsAdmin)));
            if (isAdmin && userReportParam.UserId > 0)
                user = uow.UserRepository.Get(i => i.xIsActive && i.Id == userReportParam.UserId, null, "xUserLoginHistories,xUserRoles").FirstOrDefault();


            var SimpleUserList = user.xUserLoginHistories.Where(i =>
                                                 ((userReportParam.StartDate != null) ? i.xRegisterDate.Date >= userReportParam.StartDate.Value.Date : true) &&
                                                 ((userReportParam.EndDate != null) ? i.xRegisterDate.Date < userReportParam.EndDate.Value.Date : true) &&
                                                 ((xStartTime != null) ? i.xRegisterDate.TimeOfDay >= xStartTime.Value.TimeOfDay : true) &&
                                                 ((xEndTime != null) ? i.xRegisterDate.TimeOfDay < xEndTime.Value.TimeOfDay : true)
                                               );

            return new SimpleUserListVM
            {
                xFirstName = user.xFirstName,
                xUserLoginHistories = SimpleUserList.Select(i => new UserLoginHistoryVM { xRegisterDate = i.xRegisterDate }).ToList()
            };
        }

        public async Task<TViewModel> Get<TViewModel>(long xID)
        {
            var result = await uow.UserRepository.GetFirstAsync<TViewModel>(x => x.Id == xID);
            return result;
        }

        public long GetCurrentUserID()
        {
            if (IsLogin())
            {
                long xResult;
                if (long.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out xResult))
                    return xResult;
                else
                    return 0;
            }
            else
                return 0;

        }

        public long GetCurrentPersonID()
        {
            if (IsLogin())
            {
                //TODO: retrieve from claims!
                return uow.UserRepository.GetFirst<UserSummaryVM>(x => x.Id == GetCurrentUserID()).xID;
            }
            else
                return 0;

        }

        public async Task<UserSummaryVM> GetCurrentUserAsync()
        {
            if (IsLogin())
            {
                var xModel = await uow.UserRepository.GetFirstAsync<UserSummaryVM>(x => x.Id == GetCurrentUserID());
                if (xModel != null)
                    return xModel;
                else
                    return null;
            }
            return null;
        }
        #endregion

        #region == Post Data ==
        public async Task<List<string>> AddAsync(UserRegisterVm model)
        {
            List<string> xErrors = new List<string>();

            if (model.xPassword != model.xRePassword)
                throw new ErrorMessageException("تکرار رمز عبور صحیح نمیباشد");

            var xDbModel = new User
            {
                UserName = model.xUsername,
                Email = model.xEmail,
                PhoneNumber = model.xMobile,
                xRegisterDate = DateTime.UtcNow,
                xIsActive = true,
                xFirstName = model.xFirstName,
                xLastName = model.xLastName
            };

            xDbModel.xUserRoles = new List<UserRole>();
            var xResult = await _UserManager.CreateAsync(xDbModel, model.xPassword);
            if (!xResult.Succeeded)
            {
                xErrors = xResult.Errors.Select(err =>
                {
                    return Regex.Replace(err.Description, @"Name\s?\w+\s?is already taken", string.Format("شناسه کاربری {0} قبلا انتخاب شده است", model.xUsername))
                            .Replace("Passwords must be at least 6 characters.", "رمز عبور حداقل باید 6 کاراکتر باشد .<br/>")
                            .Replace("Passwords must have at least one non letter or digit character.", " رمز عبور باید دارای کارکتر های خاص و اعداد باشد .<br/>")
                            .Replace("Passwords must have at least one digit ('0'-'9').", "رمز عبور باید دارای حداقل یک عدد باشد  (0-9) .<br/>")
                            .Replace("Passwords must have at least one lowercase ('a'-'z').", "رمز عبور باید دارای حداقل یک حرف کوچک باشد  (a-z) .<br/>")
                            .Replace("Passwords must have at least one uppercase ('A'-'Z').", "رمز عبور باید دارای حداقل یک حرف بزرگ باشد  (A-Z) .<br/>");
                }).ToList();

            }
            return xErrors;
        }
        #endregion

        #region == Put Data ==
        public async Task<bool> EditAsync(UserRegisterVm model)
        {
            var dbUser = uow.UserRepository.Get(x => x.Id == model.xID, null).FirstOrDefault();
            if (dbUser == null)
                throw new ErrorMessageException("کاربر انتخاب شده در سیستم موجود نمیباشد");

            dbUser.xFirstName = model.xFirstName;
            dbUser.xLastName = model.xLastName;
            dbUser.UserName = model.xUsername;
            dbUser.xRegisterDate = DateTime.Now;
            await uow.SaveChangesAsync();
            return true;
        }


        public async Task<bool> EditPasswordAsync(UserEditPasswordVM model)
        {

            var dbUser = uow.UserRepository.Get(x => x.Id == model.xID).FirstOrDefault();
            if (dbUser == null)
                throw new ErrorMessageException("کاربر انتخاب شده در سیستم موجود نمیباشد");

            if (await _UserManager.CheckPasswordAsync(dbUser, model.xCurrentPassword))
            {
                if (string.IsNullOrEmpty(model.xPassword) || model.xPassword != model.xRePassword)
                {
                    throw new ErrorMessageException("رمز عبور وارد شده یکسان نمی باشد");
                }

                var token = await _UserManager.GeneratePasswordResetTokenAsync(dbUser);
                await _UserManager.ResetPasswordAsync(dbUser, token, model.xPassword);

                return true;
            }
            else
            {
                throw new ErrorMessageException("رمز عبور فعلی اشتباه است");
            }
        }
        #endregion

        #region == Delete Data ==
        public async Task<bool> DeleteAsync(long xID)
        {
            var xUser = await uow.UserRepository.GetByIDAsync(xID);
            uow.UserRepository.Delete(xUser);
            await uow.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(long[] xIDs)
        {
            var xDbUsers = (await uow.UserRepository.GetAsync(x => xIDs.Contains(x.Id), null, "xPerson,xUserRoles")).ToList();
            List<string> xErrors = new List<string>();
            foreach (var xUser in xDbUsers)
            {
                bool xHasRole = false;
                xHasRole = xUser.xUserRoles.Any();
            }
            if (xErrors.Any())
            {
                throw new ErrorMessageException(xErrors);
            }
            return true;
        }
        #endregion

        #region == Other ==
        public bool IsLogin()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public async Task LogOutAsync()
        {
            await _SignInManager.SignOutAsync();
        }

        public bool CheckPermission(string xControllerName, string xActionName, long? xUserID = null)
        {
            if (xUserID == null)
                xUserID = GetCurrentUserID();

            return uow.UserRepository.Any(w => w.Id == xUserID.Value && w.xUserRoles.SelectMany(x => x.xRole.xPermissionRoles)
                .Any(d => d.xPermission.xController == xControllerName && d.xPermission.xAction == xActionName));
        }

        public async Task AddUserHistory()
        {
            if (IsLogin())
            {
                var currentUserId = GetCurrentUserID();
                uow.UserLoginHistoryRepository.Insert(new UserLoginHistory { xUserId = currentUserId, xRegisterDate = DateTime.Now });
                await uow.SaveChangesAsync();
            }
        }
        #endregion
    }
}
