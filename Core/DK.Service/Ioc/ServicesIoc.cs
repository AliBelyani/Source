using DK.Service.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DK.Service.Ioc
{
    public static class ServicesIoc
    {
        public static void AddServices(this IServiceCollection services )
        {
            List<Type> xAllServices = typeof(BaseService).Assembly.GetTypes().Where(w => !w.IsAbstract && !w.IsInterface && typeof(BaseService).IsAssignableFrom(w) && w != typeof(BaseService)).ToList();
            foreach (var item in xAllServices)
            {
                var xInterface = item.GetInterfaces().FirstOrDefault(f => f.Name.Contains(item.Name));
                if(xInterface != null)
                {
                    services.AddScoped(xInterface, item);
                }
            }
        }
    }
}
