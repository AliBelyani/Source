using DK.Domain.DTO.Security.Roles;
using DK.Domain.Entity.Security;
using System.Collections.Generic;

namespace DK.Domain.DTO.Security.Users
{
    [DtoFor(typeof(User))]
    public class UserTestVM
    {
        [MapFrom("UserName")]
        public string xUsername { get; set; }

        [MapFrom("xUserRoles.xRole")]
        public List<RoleVM> xRoles { get; set; }

        [MapFrom("xPerson.xFirstName")]
        public string xPersonName {get;set;}


    }
}
