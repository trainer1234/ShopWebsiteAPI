using SendGrid;
using SendGrid.Helpers.Mail;
using ShopWebsite.BLL.Contracts;
using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.Common.Models.EmailModels.SendGrid;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.BLL.Implementations
{
    public class EmailService : IEmailService
    {
        public async Task<Result<bool>> SendEmail(EmailSender sender, string apiKey)
        {
            //var apiKey = "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";
            // var apiKey = sender.ApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("karma.doityourself@gmail.com", "EShopee");
            var to = new EmailAddress(sender.ReceiverEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, sender.Subject, sender.PlainContent, sender.HtmlContent);
            var response = await client.SendEmailAsync(msg);
            return new Result<bool>()
            {
                Succeed = true,
                Content = true
            };
        }
    }
}
