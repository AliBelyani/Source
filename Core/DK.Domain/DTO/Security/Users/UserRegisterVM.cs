using DK.Domain.Entity.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Domain.DTO.Security.Users
{
    //[DtoFor(typeof(User))]
    public class UserRegisterVm
    {
        public long xID { get; set; }
        
        /// <summary>
        /// نام کاربری
        /// </summary>
        public string xUsername { get; set; }
        public string xFirstName { get; set; }
        public string xLastName { get; set; }
        
        /// <summary>
        /// رمز عبور
        /// </summary>
        public string xPassword { get; set; }
        
        /// <summary>
        /// ورود مجدد پسورد
        /// </summary>
        public string xRePassword { get; set; }
        
        public string xEmail { get; set; }

        public string xMobile { get; set; }

        public DateTime xRegisterDate { get; set; }
    }
}
