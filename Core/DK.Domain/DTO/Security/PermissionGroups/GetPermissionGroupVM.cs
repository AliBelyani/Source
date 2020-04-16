using DK.Domain.DTO.Security.Permissions;
using DK.Domain.Entity.Security;
using DK.Domain.Enumeration.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Domain.DTO.Security.PermissionGroups
{
    [DtoFor(typeof(PermissionGroup))]
    public class GetPermissionGroupVM
    {
        public GetPermissionGroupVM()
        {
            xPermissionGroups = new List<GetPermissionGroupVM>();
            xPermissions = new List<GetPermissionVM>();
        }
        public long xID { get; set; }
        public long? xParentID { get; set; }

        public string xName { get; set; }

        public string xControllerName { get; set; }

        public List<GetPermissionVM> xPermissions { get; set; }

        public List<GetPermissionGroupVM> xPermissionGroups { get; set; }

        [DtoFor(typeof(Permission))]
        public class GetPermissionVM
        {
            public long xID { get; set; }
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

            public bool xIsSelected { get; set; }

            public List<GetPermissionRoleVM> xPermissionRoles { get; set; }

            public ActionType xActionType { get; set; }

            public string xActionTypeString
            {
                get
                {
                    return "";
                }
            }

            public long? xPermissionGroupID { get; set; }
            [DtoFor(typeof(PermissionRole))]
            public class GetPermissionRoleVM
            {
                public long xPermissionID { get; set; }

                public long xRoleID { get; set; }
            }
        }
    }
}
