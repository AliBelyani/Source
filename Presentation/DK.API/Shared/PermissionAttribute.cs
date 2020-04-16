using DK.Domain.Enumeration.Security;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DK.API
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class PermissionAttribute : AuthorizeAttribute
    {
        public string xName { get; }
        public ActionType xActionType { get; }

        public PermissionAttribute(string xName, ActionType xActionType) : base("Permission")
        {
            this.xName = xName;
            this.xActionType = xActionType;
        }
    }

   
}
