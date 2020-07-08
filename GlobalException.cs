using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using NetCoreDemo.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetCoreDemo
{
    public class GlobalException
    {
        private static readonly Logger log = LogManager.GetLogger("logger");

        public static void ExceptionHandler(IApplicationBuilder errApp)
        {
            errApp.Run(async context =>
            {
                var feature = context.Features.Get<IExceptionHandlerFeature>();
                context.Response.StatusCode = 200;
                context.Response.ContentType = "application/json; charset=utf-8";
                if (feature != null)
                {
                    log.Error(feature.Error);
                    await context.Response.WriteAsync(new ApiBaseModel { Msg = feature.Error.Message, Status = false, Result = null, }.ToString());
                }
            });
        }
    }
}
