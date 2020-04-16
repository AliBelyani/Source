using DK.Domain.Entity.Base;

namespace DK.Domain.Entity.Security
{
    public class PermissionRole : IEntity
    {
        #region == Field ==
        public long xPermissionID { get; set; }
        public Permission xPermission { get; set; }
        public long xRoleID { get; set; }
        #endregion

        #region == Navigation ==
        public Role xRole { get; set; }
        #endregion
    }
}
