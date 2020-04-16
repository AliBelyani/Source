using DK.Domain.Entity.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DK.Service.Service.Security
{
    public class ApplicationSignInManager : SignInManager<User>
    {
        #region == Ctor ==
        public ApplicationSignInManager(UserManager<User> userManager, IHttpContextAccessor contextAccessor,
          IUserClaimsPrincipalFactory<User> claimsFactory, IOptions<IdentityOptions> optionsAccessor,
          ILogger<SignInManager<User>> logger, IAuthenticationSchemeProvider schemes)
              : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        { }
        #endregion

        #region == Other ==
        public override async Task<bool> CanSignInAsync(User user)
        {
            if (!user.xIsActive)
                return false;
            return await base.CanSignInAsync(user);
        } 
        #endregion
    }
}
