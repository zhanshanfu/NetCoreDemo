using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace NetCoreDemo
{
    public static class ServiceRegistered
    {
        public static IServiceCollection ServeRegistered(this IServiceCollection service)
        {
            var dataAccessTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Name.EndsWith("Service")).ToList();
            dataAccessTypes.ForEach(t => service.AddTransient(t));
            return service;
        }
    }
}
