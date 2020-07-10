using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreDemo.Utils;
using Microsoft.AspNetCore.Mvc;
using NetCoreDemo.Models;

namespace NetCoreDemo.Filter
{
    public class AuthFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var login = context.HttpContext.Request.ReadJWTCookie();
            if (login != null && login.Expires > DateTime.Now)
                return;
            context.Result = new UnauthorizedResult();
        }
    }
    public class AuthAttribute : TypeFilterAttribute
    {
        public AuthAttribute() : base(typeof(AuthFilter))
        {

        }
    }
}
