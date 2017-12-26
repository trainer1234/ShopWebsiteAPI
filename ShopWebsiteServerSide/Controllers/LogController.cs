using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopWebsite.BLL.Contracts;
using ShopWebsite.Common.Models.BaseModels;
using ShopWebsiteServerSide.Models.LogModels;
using ShopWebsiteServerSide.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsiteServerSide.Controllers
{
    [Authorize]
    [Route("api/log")]
    public class LogController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("error")]
        public async Task<IActionResult> GetErrorLogAsync()
        {
            var logService = GetService<ILogService>();
            var serviceResult = await logService.GetErrorLogs();
            if (serviceResult.Succeed)
            {
                var result = new Result<List<ErrorLogViewModel>>();
                var parser = new ModelParser();
                var errorLogViews = new List<ErrorLogViewModel>();
                foreach (var log in serviceResult.Content)
                {
                    var errorLogView = parser.ParseErrorLogViewFrom(log);
                    errorLogViews.Add(errorLogView);
                }
                result.Content = errorLogViews;
                result.Succeed = true;
                return Ok(result);
            }
            else
            {
                return BadRequest(serviceResult);
            }
        }
    }
}
