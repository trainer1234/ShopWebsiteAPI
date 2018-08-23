using Microsoft.AspNetCore.Mvc;
using ShopWebsite.BLL.Contracts;
using ShopWebsite.Common.Models.EmailModels.SendGrid;
using ShopWebsite.Common.Models.ServerOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsiteServerSide.Controllers
{
    [Produces("application/json")]
    [Route("api/email")]
    public class EmailController : BaseController
    {
        public EmailController(IEmailService emailService)
        {
        }

        [HttpPost]
        [Route("send")]
        public IActionResult SendEmail([FromBody] EmailSender sender)
        {
            if (ModelState.IsValid)
            {
                var emailService = GetService<IEmailService>();
                var key = EmailOption.SendGridApiKey;
                var result = emailService.SendEmail(sender, key);
                return Ok(result.Result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
