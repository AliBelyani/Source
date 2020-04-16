using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Domain.DTO.Security.Roles
{
    public class RolePermissionVM
    {
        [MapFrom("Name")]
        public string xName { get; set; }
        public string xDescription { get; set; }
    }
}
