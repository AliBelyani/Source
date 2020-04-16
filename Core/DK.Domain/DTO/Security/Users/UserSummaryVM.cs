using DK.Domain.Entity.Security;
using System;

namespace DK.Domain.DTO.Security.Users
{

    [DtoFor(typeof(User))]
    public class UserSummaryVM
    {
        [MapFrom("Id")]
        public long xID { get; set; }

        [MapFrom("UserName")]
        public string xUsername { get; set; }

        public string xFirstName { get; set; }

        public string xLastName { get; set; }
    }
}
