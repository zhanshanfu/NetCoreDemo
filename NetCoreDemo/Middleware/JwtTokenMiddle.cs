using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetCoreDemo.Models;
using NetCoreDemo.Utils;

namespace NetCoreDemo.Middleware
{
    public class JwtTokenMiddle
    {
        private readonly RequestDelegate _next;
        public JwtTokenMiddle(RequestDelegate next)
        {

            this._next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var _jwt = (GenericJwtTokenBase)context.RequestServices.GetService(typeof(GenericJwtTokenBase));
            _jwt.Jwt = context.Request.ReadJWTCookie();
            await _next(context);
        }
    }
}
