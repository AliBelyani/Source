using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Domain
{
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class MapFromAttribute : Attribute
    {
        public MapFromAttribute()
        {

        }

        public MapFromAttribute(string _PropertyName)
        {
            this.PropertyName = _PropertyName;
        }
        public string PropertyName { get; set; }

    }
}
