using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreDemo.Service;

namespace NetCoreDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : BaseController
    {
        private readonly RedisService redisService;
        public RedisController(RedisService redisService)
        {
            this.redisService = redisService;
        }
        [HttpGet(), Route("redis_test")]
        public IActionResult RedisTest()
        {
            var ip = HttpContext.Connection.RemoteIpAddress.ToString();
            var r = redisService.Buy(ip);
            if (r)
            {
                return ApiSuccess();
            }
            return ApiFail("库存不足");
        }
    }
}