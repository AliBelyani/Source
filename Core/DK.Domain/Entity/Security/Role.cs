using DK.Domain.Entity.Base;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DK.Domain.Entity.Security
{
    public class Role : IdentityRole<long>, IEntity
    {
        #region == Field ==
        public string xDescription { get; set; }
        public bool xIsAdmin { get; set; }
        #endregion

        #region == Navigation ==
        public ICollection<PermissionRole> xPermissionRoles { get; set; }
        #endregion
    }
}
