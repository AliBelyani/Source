using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Domain
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class DtoForAttribute : Attribute
    {
        public DtoForAttribute(Type _EntityClass)
        {
            this.EntityClass = _EntityClass;
        }

        public DtoForAttribute()
        {

        }
        public Type EntityClass { get; set; }
        
    }
}
