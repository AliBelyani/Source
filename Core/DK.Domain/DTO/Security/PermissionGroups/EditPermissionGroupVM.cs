using DK.Domain.DTO.Security.Permissions;
using DK.Domain.Entity.Security;
using DK.Domain.Enumeration.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Domain.DTO.Security.PermissionGroups
{
    [DtoFor(typeof(PermissionGroup))]
    public class EditPermissionGroupVM
    {
        public List<GetPermissionVM> xPermissions { get; set; }


        [DtoFor(typeof(Permission))]
        public class GetPermissionVM
        {
            public long xID { get; set; }
        
            public bool xIsSelected { get; set; }
            
        }
    }
}
