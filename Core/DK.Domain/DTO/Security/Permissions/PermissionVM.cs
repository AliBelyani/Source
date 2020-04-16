using DK.Domain.Enumeration.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Domain.DTO.Security.Permissions
{
    public class PermissionVM
    {
        /// <summary>
        /// نام دسترسی
        /// </summary>
        public string xName { get; set; }

        /// <summary>
        /// کنترلر دسترس
        /// </summary>
        public string xController { get; set; }

        /// <summary>
        /// اکشن دسترس
        /// </summary>
        public string xAction { get; set; }

        public ActionType xActionType { get; set; }

        public string xComment { get; set; }

        public long? xPermissionGroupID { get; set; }

    }
}
