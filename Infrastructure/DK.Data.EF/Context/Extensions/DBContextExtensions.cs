using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DK.Data.EF.Context.Extensions
{
    internal static class DBContextExtensions
    {
        public static void AddDBSetFromModel(this ModelBuilder modelBuilder, Assembly assemblyToSearch, Type TEntity)
        {
            foreach (var xAssemblyType in assemblyToSearch.GetTypes().Where(w => !w.IsAbstract && !w.IsInterface))
            {

                if (!xAssemblyType.IsAbstract && !xAssemblyType.IsInterface)
                {     
                    if (TEntity.IsAssignableFrom(xAssemblyType))
                    {
                        modelBuilder.Entity(xAssemblyType);
                    }
                }
            }
        }
    }
}
