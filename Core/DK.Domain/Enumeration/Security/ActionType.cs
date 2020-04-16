using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DK.Domain.Enumeration.Security
{
    public enum ActionType
    {
        [Description("نمایش")]
        Read,

        [Description("افزودن")]
        Insert,

        [Description("ویرایش")]
        Update,

        [Description("حذف")]
        Delete
    }
}
