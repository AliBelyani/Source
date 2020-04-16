using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Domain.Entity.Security
{
    public class UserToken: IdentityUserToken<long>
    {
    }
}
