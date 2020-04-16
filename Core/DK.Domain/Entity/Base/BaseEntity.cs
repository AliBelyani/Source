using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Domain.Entity.Base
{
    public abstract class BaseEntity<T> : IEntity<T>, IEntity
    { 
        public T xID { get; set; }
    }

    public interface IEntity
    { }
    public interface IEntity<T>
    {
        T xID { get; set; }
    }

    public interface ISoftDelete
    {
        bool xIsDeleted { get; set; }
    }

    public interface IRegisterDate<TRegistererID>
    {
        DateTime xRegisterDate { get; set; }
        TRegistererID xRegistererID { get; set; }
    }
    public interface IModifiedDate<TModifierID>
    {
        DateTime xModifyDate { get; set; }
        TModifierID xModifierID { get; set; }
    }
}
