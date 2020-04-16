using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DK.Domain.DTO.Security.PermissionGroups;
using DK.Domain.DTO.Security.Permissions;
using DK.Domain.Enumeration.Security;
using DK.Service.Service.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace DK.API.Controllers.Security
{
    [Description("دسترسی")]
    public class PermissionController : ApiControllerBase
    {
        #region == Ctor ==
        private readonly IPermissionService _permissionService;
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        public PermissionController(IPermissionService permissionService, IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _permissionService = permissionService;
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }
        #endregion

        #region == Read Data ==
        [HttpGet("{xRoleID}")]
        //[Permission("دریافت دسترسی ها", ActionType.Read)]
        public async Task<GeneralResult> GetAll([FromRoute]long xRoleID)
        {
            return Ok(await _permissionService.GetsForRole(xRoleID));
        }

        [HttpGet("UpdatePermissions")]
        //[Permission("دریافت تمام دسترسی های سیستم", ActionType.Read)]
        public async Task<GeneralResult> UpdatePermissions()
        {
            var xTotalList = _actionDescriptorCollectionProvider.ActionDescriptors.Items.OfType<ControllerActionDescriptor>().Where(w => w.MethodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(PermissionAttribute))).ToList();
            var xPermissionGroups = xTotalList.Select(z => z.ControllerName).Distinct().Select(xControllerName => new PermissionGroupVM
            {
                xName = xTotalList.Where(w => w.ControllerName == xControllerName).FirstOrDefault().ControllerTypeInfo.CustomAttributes.Any() ? xTotalList.Where(w => w.ControllerName == xControllerName).FirstOrDefault().ControllerTypeInfo.GetCustomAttribute<DescriptionAttribute>().Description : "بی نام",
                xControllerName = xControllerName.Replace("Controller", ""),
                xPermissions = xTotalList.Where(w => w.ControllerName == xControllerName).Select(xAction => new PermissionVM
                {
                    xAction = xAction.ActionName,
                    xActionType = xAction.MethodInfo.GetCustomAttribute<PermissionAttribute>().xActionType,
                    xController = xControllerName,
                    xName = xAction.MethodInfo.GetCustomAttribute<PermissionAttribute>().xName
                }).ToList()
            }).ToList();
            return Ok(await _permissionService.UpdateList(xPermissionGroups));
        }
        #endregion

        #region == Put Data ==
        [HttpPut("{xRoleID}")]
        //[Permission("ویرایش دسترسی", ActionType.Update)]
        public async Task<GeneralResult> Put([FromRoute] long xRoleID, [FromBody] List<long> xSelectedPermissionIDs)
        {
            return Ok (await _permissionService.EditRolePermission(xRoleID, xSelectedPermissionIDs));
        } 
        #endregion
    }
}
