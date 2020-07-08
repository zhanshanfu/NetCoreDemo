using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreDemo.Middleware
{
    public static class MiddlewareExpansion
    {
        public static void UseLogger(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<LoggerMiddle>();
        }
        public static void UseJwtToken(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<JwtTokenMiddle>();
        }
    }
}
