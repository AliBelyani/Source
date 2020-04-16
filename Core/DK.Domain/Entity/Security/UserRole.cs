using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Domain.Entity.Security
{
    public class UserRole: IdentityUserRole<long>
    {
        public  User xUser { get; set; }
        public  Role xRole { get; set; }
    }
}
