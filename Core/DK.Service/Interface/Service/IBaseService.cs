using DK.Domain.Entity.Base;
using DK.Service.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Service.Interface.Base.Service
{
    public interface IBaseService<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
    }


}
