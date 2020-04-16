using DK.Domain.Entity.Base;
using DK.Domain.Enumeration.Security;
using System.Collections.Generic;

namespace DK.Domain.Entity.Security
{
    public class Permission:BaseEntity<long>
    {
        #region == Field ==
        /// <summary>
        /// نام دسترسی
        /// </summary>
        public string xName { get; set; }

        /// <summary>
        /// کنترلر دسترس
        /// </summary>
        public string xController { get; set; }

        /// <summary>
        /// اکشن دسترس
        /// </summary>
        public string xAction { get; set; }

        public string xComment { get; set; }

        public ActionType xActionType { get; set; }

        public long xPermissionGroupID { get; set; }
        #endregion

        #region == Navigation ==
        public PermissionGroup xPermissionGroup { get; set; }

        public ICollection<PermissionRole> xPermissionRoles { get; set; }
        #endregion
    }
}
