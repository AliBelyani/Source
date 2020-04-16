using DK.Domain.DTO.Security.Permissions;
using DK.Domain.Entity.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Domain.DTO.Security.PermissionGroups
{
    [DtoFor(typeof(PermissionGroup))]
    public class PermissionGroupVM
    {
        public long? xParentID { get; set; }

        public string xName { get; set; }

        public string xControllerName { get; set; }

        public List<PermissionVM> xPermissions { get; set; }
    }
}
