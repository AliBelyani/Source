using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Domain.DTO.Security.Users
{
    public class UserSearchParameter
    {

        public string xUsername { get; set; }

        public string xEmail { get; set; }
        public string xFirstName { get; set; }
        public string xLastName { get; set; }
        public bool? xIsActive { get; set; } = null;

        //public string xMobile { get; set; }

        public string xSortBy { get; set; } = "Date";
        public string xSortType { get; set; } = "Des";
        public int xPage { get; set; } = 1;
        public int xPageSize { get; set; } = 25;

    }
}
