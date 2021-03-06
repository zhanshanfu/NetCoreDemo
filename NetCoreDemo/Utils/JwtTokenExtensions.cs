﻿using Microsoft.AspNetCore.Http;
using NetCoreDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreDemo.Utils
{
    public static class JwtTokenExtensions
    {
        public static GenericJwtToken ReadJWTCookie(this HttpRequest request)
        {
            try
            {
                string jwt = request.Headers["Authorization"].ToString();
                var login = AuthJwtEncoder.Decode<GenericJwtToken>(jwt);
                return login;
            }
            catch (Exception) { }
            return new GenericJwtToken();
        }
    }
}
