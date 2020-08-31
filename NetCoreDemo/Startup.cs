using CSRedis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using NetCoreDemo.DB.Models;
using NetCoreDemo.Middleware;
using NetCoreDemo.SignalRChat;
using NetCoreDemo.Tools;
using NetCoreDemo.Tools.Redis;
using NetCoreDemo.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace NetCoreDemo
{
    public class Startup
    {

        private const string c_CorsPolicy = "Cors";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddMemoryCache();
            services.AddSignalR();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<testContext>(options =>
                    options.UseMySql(Configuration.GetValue<string>("MySqlConnection")));

            services.AddSingleton(context =>
            {
                var config = (ConfigExtensions)context.GetService(typeof(ConfigExtensions));
                return new RedisContext(config.GetConfig<RedisOptions>().Connect);
            });
            services.AddSingleton<ConfigExtensions>();
            // 注册服务
            services.ServeRegistered();
            //从请求中读取 JWT
            services.AddScoped(context =>
            {
                var httpContextAccessor = (IHttpContextAccessor)context.GetService(typeof(IHttpContextAccessor));
                return httpContextAccessor.HttpContext.Request.ReadJWTCookie();
            });
            //配置跨域
            //services.AddCors(options =>
            //        options.AddPolicy("Cors",
            //            corsBuilder =>
            //                corsBuilder
            //                //    .SetIsOriginAllowed(url => url.Equals("http://10.104.14.144:8002"))
            //                    .WithOrigins("*")
            //                    .AllowAnyMethod()
            //                    .AllowAnyHeader()
            //                    .AllowCredentials())
            //        );
            var hosts = new List<string>();
            Configuration.GetSection(c_CorsPolicy).Bind(hosts);
            services.AddCors(optiopns =>
            {
                optiopns.AddPolicy(c_CorsPolicy, build =>
                {
                    build.WithOrigins(hosts.ToArray())
                    .AllowAnyMethod().AllowAnyHeader().AllowCredentials()
                    .SetIsOriginAllowedToAllowWildcardSubdomains();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler(GlobalException.ExceptionHandler);

            app.UseLogger();

            //app.UseAuthentication();

            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider=new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot")),
                RequestPath="/wwwroot"
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chathub");
            });
        }
    }
}
