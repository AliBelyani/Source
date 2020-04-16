using DK.Domain.Entity.Security;
using System;
using System.Collections.Generic;

namespace DK.Domain.DTO.Security.Users
{

    [DtoFor(typeof(User))]
    public class UserVM
    {
        [MapFrom("Id")]
        public long xID { get; set; }

        [MapFrom("UserName")]
        public string xUsername { get; set; }
        public string xFirstName { get; set; }
        public string xLastName { get; set; }
    }
}
