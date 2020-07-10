using Microsoft.AspNetCore.Http;
using NetCoreDemo.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreDemo.Models
{
    public class GenericJwtToken
    {
        public int Uid { get; set; }
        public string UserName { get; set; }
        public DateTime Expires { get; set; }
    }

    public class GenericJwtTokenBase
    {
        public GenericJwtToken Jwt { get; set; }
    }
}
