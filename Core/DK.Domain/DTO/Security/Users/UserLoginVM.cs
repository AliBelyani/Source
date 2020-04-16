using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Domain.DTO.Security.Users
{
    public class UserLoginVM
    {
        public string xUsername { get; set; }
        public string xPassword { get; set; }
        public string xCaptcha { get; set; }

    }
}
