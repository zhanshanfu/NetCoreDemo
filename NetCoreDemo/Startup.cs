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
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<testContext>(options =>
                    options.UseMySql(Configuration.GetValue<string>("MySqlConnection")));

            // 注册服务
            services.ServeRegistered();
            //从请求中读取 JWT
            services.AddScoped(context =>
            {
                var httpContextAccessor = (IHttpContextAccessor)context.GetService(typeof(IHttpContextAccessor));
                return httpContextAccessor.HttpContext.Request.ReadJWTCookie();
            });
            //配置跨域
            services.AddCors(options =>
                    options.AddPolicy("Cors",
                        corsBuilder =>
                            corsBuilder
                                .SetIsOriginAllowed(url => true)
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials())
                    );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler(GlobalException.ExceptionHandler);
            }

            app.UseLogger();

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
