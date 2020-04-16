using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DK.Utility
{
    public static class ReflectionHelper
    {
        public static bool IsEnumerable(this Type xType)
        {
            return xType != null && xType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(xType);
        }
    }
}
