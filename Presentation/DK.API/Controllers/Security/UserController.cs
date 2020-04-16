using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using DK.Domain.DTO.Security.Users;
using DK.Domain.Entity.Security;
using DK.Domain.Enumeration.Security;
using DK.Service.Service.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DK.API.Controllers.Security
{
    [Description("کاربران")]
    public class UserController : ApiControllerBase
    {
        #region == Ctor ==
        private readonly IUserService _UserService;
        private readonly IRoleService _RoleService;
        public UserController(IUserService UserService,
            IRoleService RoleService)
        {
            _UserService = UserService;
            _RoleService = RoleService;
        }
        #endregion

        #region == Read Data ==
        [HttpGet]
        [Permission("دریافت کاربران", ActionType.Read)]
        public async Task<GeneralResult> GetAll([FromQuery] UserSearchParameter xParams)
        {
            return Ok(await _UserService.GetsAsync<UserListVM>(true, xParams));
        }

        [HttpGet("GetAllSimple")]
        [Permission("دریافت کاربران", ActionType.Read)]
        public async Task<GeneralResult> GetAllSimple()
        {
            return Ok(await _UserService.GetsSimpleAsync<UserListVM>());
        }

        [HttpGet("{xID}")]
        [Permission("دریافت کاربر", ActionType.Read)]
        public async Task<GeneralResult> Get(long xID)
        {
            var xResult = await _UserService.Get<UserVM>(xID);
            if (xResult == null)
                return Ok(null, new List<string> { "کاربر مورد نظر در سیستم موجود نمیباشد" });
            return Ok(xResult);
        }

        [HttpGet("PrepareAdd")]
        [Permission("دریافت کاربر", ActionType.Read)]
        public async Task<GeneralResult> PrepareAdd()
        {
            var roleSelectionList = await _RoleService.GetAll();

            return Ok(roleSelectionList);
        }

        [HttpGet("PreparePut/{xID}")]
        [Permission("دریافت کاربر", ActionType.Read)]
        public async Task<GeneralResult> PreparePut(long xID)
        {
            var xResult = await _UserService.Get<UserVM>(xID);
            if (xResult == null)
                return Ok(null, new List<string> { "کاربر مورد نظر در سیستم موجود نمیباشد" });

            var roleSelectionList = await _RoleService.GetAll();

            return Ok(new
            {
                xUser = xResult,
                xRoles = roleSelectionList
            });
        }

        [HttpGet("Summary")]
        [Permission("دریافت اطلاعات کاربر", ActionType.Read)]
        public async Task<GeneralResult> GetUserSummary()
        {
            var xResult = await _UserService.GetCurrentUserAsync();
            if (xResult == null)
                return Ok(null, new List<string> { "خطا در دریافت اطلاعات" });
            else
                return Ok(xResult);
        }
        #endregion

        #region == Post Data ==
        [HttpPost]
        //[Permission("افزودن کاربر", ActionType.Insert)]
        public async Task<GeneralResult> Post([FromBody] UserRegisterVm model)
        {
            var xErrors = await _UserService.AddAsync(model);
            if (!xErrors.Any())
                return Ok("ثبت با موفقیت انجام شد");
            else
                return BadRequest(xErrors);
        }

        [HttpPut("Put")]
        [Permission("ویرایش کاربر", ActionType.Update)]
        public async Task<GeneralResult> Put([FromBody] UserRegisterVm model)
        {
            var status = await _UserService.EditAsync(model);
            return Ok(status);
        }

        [HttpPut("EditPassword")]
        [Permission("ویرایش رمز عبور کاربر", ActionType.Update)]
        public async Task<GeneralResult> EditPassword([FromBody] UserEditPasswordVM model)
        {
            var result = await _UserService.EditPasswordAsync(model);
            return Ok(result);
        }
        #endregion

        #region == Delete Data ==
        [HttpDelete("Deletes")]
        [Permission("حذف کاربرها", ActionType.Delete)]
        public async Task<GeneralResult> Delete([FromBody] long[] xIDs)
        {
            await _UserService.DeleteAsync(xIDs);
            return Ok(true);
        }

        [HttpDelete("Delete/{id}")]
        [Permission("حذف کاربرها", ActionType.Delete)]
        public async Task<GeneralResult> Delete(long id)
        {
            await _UserService.DeleteAsync(id);
            return Ok(true);
        }

        [HttpPost("UseLoginHistory")]
        public async Task<GeneralResult> PostUseLoginHistory()
        {
            await _UserService.AddUserHistory();
            return Ok();
        }
        #endregion

        #region Report
        [HttpPost("GetUserLoginReport")]
        [Permission("دریافت گزارشات ورود و خروج کاربران", ActionType.Read)]
        public GeneralResult GetUserLoginReport([FromBody] UserReportParam userReportParam = null)
        {
            var users =  _UserService.GetsUserReportAsync(userReportParam);
            return Ok(users);
        }
        #endregion
    }
}
