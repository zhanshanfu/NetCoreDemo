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
        private readonly GenericJwtToken genericJwtToken;
        public AuthFilter(GenericJwtToken genericJwtToken)
        {
            this.genericJwtToken = genericJwtToken;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (genericJwtToken != null && genericJwtToken.Expires > DateTime.Now)
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
