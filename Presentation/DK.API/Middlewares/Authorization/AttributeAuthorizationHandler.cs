using DK.Data.EF.Context;
using DK.Service.Service.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DK.API.Middlewares.Authorization
{
    public class AttributeAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
    {
        private readonly IUserService _UserService;

        public AttributeAuthorizationHandler(IUserService userService)
        {
            _UserService = userService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
        {
            if (context.Resource is AuthorizationFilterContext)
            {
                if (_UserService.IsLogin())
                {
                    var xFilterContext = ((AuthorizationFilterContext)context.Resource);
                    string xActionName = xFilterContext.RouteData.Values["action"].ToString();
                    string xControllerName = xFilterContext.RouteData.Values["controller"].ToString();
                    var xCurrentUserID = _UserService.GetCurrentUserID();
                    if (_UserService.CheckPermission(xControllerName, xActionName, xCurrentUserID))
                    {
                        context.Succeed(requirement);
                    }
                    else
                        context.Fail();
                }
                else
                    context.Fail();
            }
            await Task.Yield();
            
        }
    }
}
