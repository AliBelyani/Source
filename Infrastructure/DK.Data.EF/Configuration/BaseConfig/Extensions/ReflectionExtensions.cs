using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace DK.Data.EF.Configuration.Base.Extensions
{
    public static class ReflectionExtensions
    {
        public static bool IsAssignableFromGeneric(this Type GenericInterface, Type objectType)
        {
            foreach (var xInterface in objectType.GetInterfaces())
            {
                if (xInterface.IsGenericType && xInterface.GetGenericTypeDefinition() == GenericInterface)
                    return true;
            }
            return false;
        }
        
        public static Type[] GetDerivedInterfaceGenericArgumentType(this Type type, Type interfaceToSearch)
        {
            return type.GetInterfaces().FirstOrDefault(f => f.IsGenericType && f.GetGenericTypeDefinition() == interfaceToSearch).GetGenericArguments();
        }
    }
}
