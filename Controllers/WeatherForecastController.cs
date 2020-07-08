using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreDemo.Models;
using NetCoreDemo.Utils;
using NetCoreDemo.Filter;

namespace NetCoreDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : BaseController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly GenericJwtToken genericJwtToken;

        public WeatherForecastController(GenericJwtTokenBase genericJwtTokenBase)
        {
            this.genericJwtToken = genericJwtTokenBase.Jwt;
        }
        [Auth]
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var username = genericJwtToken.UserName;
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]LoginUser input)
        {
            //throw new Exception("我是一个异常");
            //从数据库验证用户名，密码 
            //验证通过 否则 返回Unauthorized
            var jwtToken = new GenericJwtToken
            {
                Uid = 1,
                UserName = input.Username,
                Expires = DateTime.Now.AddHours(2)
            };
            //返回token和过期时间
            return ApiSuccess(new { jwtToken = AuthJwtEncoder.Encode(jwtToken), jwtToken.Expires });
        }
    }
}
