using DK.Domain.Entity.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Domain.DTO.Security.Users
{
    //[DtoFor(typeof(User))]
    public class UserEditPasswordVM
    {
        public long xID { get; set; }

        /// <summary>
        /// رمز عبور فعلی
        /// </summary>
        public string xCurrentPassword { get; set; }
        
        /// <summary>
        /// رمز عبور
        /// </summary>
        public string xPassword { get; set; }
        
        /// <summary>
        /// ورود مجدد پسورد
        /// </summary>
        public string xRePassword { get; set; }
    }
}
