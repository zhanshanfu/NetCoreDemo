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
        private readonly ILogger<RequestLogMiddle> logger;
        public RequestLogMiddle(RequestDelegate next, ILogger<RequestLogMiddle> logger)
        {
            this._next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            logger.LogInformation($"请求路径：{context.Request.Path + context.Request.QueryString.Value}");
            if (!context.Request.Method.Equals("GET"))
            {
                context.Request.EnableBuffering();
                using (var data = new StreamReader(context.Request.Body, Encoding.UTF8, false, 1024, true))
                {
                    var body = await data.ReadToEndAsync();
                    logger.LogInformation($"请求 body：{body}");
                    context.Request.Body.Seek(0, SeekOrigin.Begin);
                }
            }

            await _next(context);
        }
    }
}
