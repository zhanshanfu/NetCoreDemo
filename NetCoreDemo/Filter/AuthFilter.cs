using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreDemo.Utils;
using Microsoft.AspNetCore.Mvc;
using NetCoreDemo.Models;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetCoreDemo.Filter
{
    public class AuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var genericJwtToken = (GenericJwtToken)context.HttpContext.RequestServices.GetService(typeof(GenericJwtToken));
            if (genericJwtToken != null && genericJwtToken.Expires > DateTime.Now)
                return;
            context.Result = new UnauthorizedResult();
        }
    }
}
