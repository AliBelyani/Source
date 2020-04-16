using DK.Domain.DTO.Security.Roles;
using DK.Domain.Entity.Security;
using System;
using System.Collections.Generic;

namespace DK.Domain.DTO.Security.Users
{
    [DtoFor(typeof(User))]
    public class UserListVM
    {

        [MapFrom("Id")]
        public long xID { get; set; }
        /// <summary>
        /// نام کاربری
        /// </summary>
        [MapFrom("UserName")]
        public string xUsername { get; set; }

        public bool xIsActive { get; set; }

        public string xFirstName { get; set; }

        public string xLastName { get; set; }

        public SimpleRoleVM xUserRoles { get; set; }
    }

    public class SimpleUserListVM
    {
        [MapFrom("Id")]
        public long xID { get; set; }

        [MapFrom("UserName")]
        public string xUsername { get; set; }

        public string xFirstName { get; set; }

        public string xLastName { get; set; }

        public List<UserLoginHistoryVM> xUserLoginHistories { get; set; }
    }

     

    [DtoFor(typeof(UserLoginHistory))]
    public class UserLoginHistoryVM
    {
        public DateTime xRegisterDate { get; set; }
    }

    public class UserReportParam
    {
        public long UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
