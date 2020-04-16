using System;
using System.Collections.Generic;
using DK.Domain.Entity.Base;
using Microsoft.AspNetCore.Identity;

namespace DK.Domain.Entity.Security
{
    public class User : IdentityUser<long>, IEntity
    {
        #region == Field ==
        public string xFirstName { get; set; }
        public string xLastName { get; set; }
        public bool xIsActive { get; set; } = false;
        public DateTime xRegisterDate { get; set; }
        #endregion

        #region == Navigation ==
        public ICollection<UserRole> xUserRoles { get; set; }
        public ICollection<UserLoginHistory> xUserLoginHistories { get; set; }
        #endregion
    }
}
