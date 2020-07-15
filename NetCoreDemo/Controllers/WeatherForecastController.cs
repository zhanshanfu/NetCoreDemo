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
using NetCoreDemo.Service;
using System.IO;
using System.DrawingCore.Imaging;

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
        private readonly TestService testService;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(GenericJwtTokenBase genericJwtTokenBase, TestService testService,
            ILogger<WeatherForecastController> _logger)
        {
            this.genericJwtToken = genericJwtTokenBase.Jwt;
            this.testService = testService;
            this._logger = _logger;
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
        [HttpGet]
        [Route("get_ball")]
        public IActionResult GetBall()
        {
            testService.GetData();
            return ApiSuccess();
        }
        [HttpGet]
        [Route("test")]
        public IActionResult Tast()
        {
            var s = testService.Test();
            return ApiSuccess(s);
        }
        [Route("verify_code")]
        [HttpGet]
        public ActionResult VerifyCode()
        {
            string code = VerifyCodeHelper.GetSingleObj().CreateVerifyCode(VerifyCodeHelper.VerifyCodeType.MixVerifyCode);
            //这个Code 存缓存或者数据库都行  需要和后台对比
            _logger.LogInformation("这是code" + code);
            var bitmap = VerifyCodeHelper.GetSingleObj().CreateBitmapByImgVerifyCode(code, 100, 40);
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Gif);
            return File(stream.ToArray(), "image/gif");
        }

    }
}

