using DK.Domain.DTO.Security.Permissions;
using DK.Domain.Entity.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Domain.DTO.Security.Roles
{
    [DtoFor(typeof(Role))]
    public class RoleVM
    {
        [MapFrom("Id")]
        public long xID { get; set; }
        [MapFrom("Name")]
        public string xName { get; set; }
        public string xDescription {get;set;}

        [MapFrom("xPermissionRoles.xPermission")]
        public List<PermissionVM> xPermissions { get; set; }
    }

    [DtoFor(typeof(Role))]
    public class SimpleRoleVM
    {
        [MapFrom("Id")]
        public long xID { get; set; }
        [MapFrom("Name")]
        public string xName { get; set; }
        public bool xIsAdmin { get; set; }
    }
}
