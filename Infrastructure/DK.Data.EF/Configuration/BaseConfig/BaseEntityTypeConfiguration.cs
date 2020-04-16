using DK.Domain.Entity.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Data.EF.Configuration
{
    public interface IBaseEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : class
    {
        
    }
}
