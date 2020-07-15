using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreDemo.Service;

namespace NetCoreDemo
{
    public static class ServiceRegistered
    {
        public static IServiceCollection ServeRegistered(this IServiceCollection service)
        {
            service.AddTransient<TestService>();
            return service;
        }

    }
}
