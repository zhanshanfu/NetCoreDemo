using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreDemo.DB.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xunit;

namespace NetCoreDemo.Test
{
    public class DemoTest
    {
        private readonly IServiceCollection service = new ServiceCollection();
        private IConfiguration Configuration;

        private testContext GetDbContext(string environment)
        {
            if (environment == null) environment = "";
            else environment = "." + environment;

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings{environment}.json");
            Configuration = builder.Build();

            service.AddDbContext<testContext>(options =>
                    options.UseMySql(Configuration.GetValue<string>("MySqlConnection")));

            ServiceProvider provider = service.BuildServiceProvider();
            var context = (testContext)provider.GetService(typeof(testContext));
            return context;
        }
        [Theory(DisplayName = "≤È—Ø≤‚ ‘")]
        [InlineData("Development")]
        public void ImportSwzId(string environment)
        {
            var context = GetDbContext(environment);
            var list = context.Ball.Take(10).ToList();
            Debugger.Break();
        }


    }
}
