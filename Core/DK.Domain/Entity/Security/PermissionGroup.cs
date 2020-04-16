using DK.Domain.Entity.Base;
using System.Collections.Generic;

namespace DK.Domain.Entity.Security
{
    public class PermissionGroup: BaseEntity<long>
    {
        #region == Field ==
        public long? xParentID { get; set; }
        public string xName { get; set; }
        public string xControllerName { get; set; }
        #endregion

        #region == Navigation ==
        public PermissionGroup xPermissionGroup { get; set; }
        public ICollection<PermissionGroup> xPermissionGroups { get; set; }
        public ICollection<Permission> xPermissions { get; set; }
        #endregion
    }
}
