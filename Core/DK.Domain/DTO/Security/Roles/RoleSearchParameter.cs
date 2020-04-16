using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Domain.DTO.Security.Roles
{
    public class RoleSearchParameter
    {
            public string xName { get; set; }
            public string xSortBy { get; set; } = "xName";
            public string xSortType { get; set; } = "Des";
            public int xPage { get; set; } = 1;
            public int xPageSize { get; set; } = 25;
    }
}
