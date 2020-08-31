using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreDemo.Middleware
{
    public class RequestLogMiddle
    {
        private readonly RequestDelegate _next;
        private readonly Logger logger = LogManager.GetLogger("logger");
        public RequestLogMiddle(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
#if RELEASE
            logger.Info($"请求路径：{context.Request.Path + context.Request.QueryString.Value}");
            if (!context.Request.Method.Equals("GET"))
            {
                context.Request.EnableBuffering();
                using (var data = new StreamReader(context.Request.Body, Encoding.UTF8, false, 1024, true))
                {
                    var body = await data.ReadToEndAsync();
                    logger.Info($"请求 body：{body}");
                    context.Request.Body.Seek(0, SeekOrigin.Begin);
                }
            }
#endif
            await _next(context);
        }
    }
}
