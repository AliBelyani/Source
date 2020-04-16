using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using DK.Domain.DTO.Security.Roles;
using DK.Domain.Entity.Security;
using DK.Domain.Enumeration.Security;
using DK.Service.Service.Security;
using Microsoft.AspNetCore.Mvc;

namespace DK.API.Controllers.Security
{
    [Description("نقش کاربر")]
    public class RoleController : ApiControllerBase
    {
        #region == Ctor ==
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        #endregion

        #region == Read Data ==
        [HttpGet("{xID}")]
       // [Permission("دریافت نقش کاربر", ActionType.Read)]
        public async Task<GeneralResult> Get(long xID)
        {
            return Ok(await _roleService.GetByID<RoleVM>(xID));
        }
        
        [HttpGet]
       // [Permission("دریافت نقش های کاربر", ActionType.Read)]
        public async Task<GeneralResult> GetAll()
        {
            return Ok((await _roleService.GetsAsync<RoleVM>(false, new RoleSearchParameter())).xData);
        }
        #endregion

        #region == Post Data ==
        [HttpPost]
     //   [Permission("افزودن نقش کاربر", ActionType.Insert)]
        public async Task<GeneralResult> Post([FromBody] RoleVM model)
        {
            var xErrors = await _roleService.AddAsync(model);
            if (xErrors.Any())
                return Ok(null, xErrors);
            return Ok(true);
        } 
        #endregion

        #region == Put Data ==
        [HttpPut]
     //   [Permission("ویرایش نقش کاربر", ActionType.Update)]
        public async Task<GeneralResult> Put([FromBody]RoleVM model)
        {
            var xErrors = await _roleService.EditAsync(model);
            if (xErrors.Any())
                return Ok(null, xErrors);
            return Ok(true);
        } 
        #endregion

        #region == Delete Data ==
        [HttpDelete("{xID}")]
     //   [Permission("حذف نقش کاربر", ActionType.Delete)]
        public async Task<GeneralResult> Delete(long xID)
        {
            var xErrors = await _roleService.DeleteAsync(xID);
            if (xErrors.Any())
                return Ok(null, xErrors);
            return Ok(true);
        } 
        #endregion
    }
}