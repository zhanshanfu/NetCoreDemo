using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetCoreDemo.DB.Models;
using NetCoreDemo.Middleware;
using NetCoreDemo.Models;
using NetCoreDemo.Utils;

namespace NetCoreDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<GenericJwtTokenBase>();
            services.AddDbContext<testContext>(options =>
                    options.UseMySql(Configuration.GetValue<string>("MySqlConnection")));

            // ×¢²á·þÎñ
            services.ServeRegistered();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseExceptionHandler(GlobalException.ExceptionHandler);
            }

            //app.Use(next =>
            //{
            //    return new RequestDelegate(
            //       async context =>
            //         {
            //             await context.Response.WriteAsync("xxx");
            //         }
            //        );
            //});
            app.UseLogger();

            app.UseJwtToken();

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
