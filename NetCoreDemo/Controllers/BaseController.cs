using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreDemo.Models;

namespace NetCoreDemo.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IActionResult ApiSuccess(object model, string msg = "success")
        {
            return Ok(new ApiBaseModel { Msg = msg, Status = true, Result = model, Total = 0 });
        }
        protected IActionResult ApiSuccess(string msg = "success")
        {
            return Ok(new ApiBaseModel { Msg = msg, Status = true, Result = null, Total = 0 });
        }
        protected IActionResult ApiSuccess(object model, int Total, string msg = "success")
        {
            return Ok(new ApiBaseModel { Msg = msg, Status = true, Result = model, Total = Total });
        }
        protected IActionResult ApiFail(string msg)
        {
            return Ok(new ApiBaseModel { Msg = msg, Status = false, Result = null, Total = 0 });
        }
    }
}