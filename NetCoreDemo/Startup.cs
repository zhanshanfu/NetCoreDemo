using CSRedis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using NetCoreDemo.DB.Models;
using NetCoreDemo.Middleware;
using NetCoreDemo.Tools;
using NetCoreDemo.Tools.Redis;
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
            services.AddMemoryCache();
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
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler(GlobalException.ExceptionHandler);

            app.UseLogger();

            //app.UseAuthentication();

            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
