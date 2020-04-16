using DK.Domain.Entity.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Domain.Entity.Security
{
    public class UserLoginHistory : BaseEntity<long>
    {
        public long xUserId { get; set; }
        public DateTime xRegisterDate { get; set; }

        public User xUser { get; set; }
    }
}
