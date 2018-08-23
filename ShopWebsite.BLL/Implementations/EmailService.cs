using SendGrid;
using SendGrid.Helpers.Mail;
using ShopWebsite.BLL.Contracts;
using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.Common.Models.EmailModels.SendGrid;
using ShopWebsite.Common.Models.ServerOptions;
using ShopWebsite.DAL.Models.ProductOrderModels;
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
            var from = new EmailAddress(EmailOption.EmailFrom, EmailOption.EmailFromName);
            var to = new EmailAddress(sender.ReceiverEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, sender.Subject, sender.PlainContent, sender.HtmlContent);
            msg.TemplateId = sender.TemplateId;
            msg.AddSubstitutions(new Dictionary<string, string>
            {
                { "&lt;%subject%&gt;", "Test subject" },
                { "&lt;%body%&gt;", "Test content" }
            });
            var response = await client.SendEmailAsync(msg);
            var t = await response.Body.ReadAsStringAsync();
            return new Result<bool>()
            {
                Succeed = true,
                Content = true
            };
        }

        public async Task<Result<bool>> SendOrderSuccessEmail(ProductOrder productOrder, EmailSender sender, string apiKey, string orderSuccessTemplateId)
        {
            //var apiKey = "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";
            // var apiKey = sender.ApiKey;

            string productOrderMailContent = "";
            foreach (var productMapOrder in productOrder.ProductMapOrderDetails)
            {
                var name = productMapOrder.Product.Name;
                var price = productMapOrder.Product.Price;
                var amount = productMapOrder.ProductAmount;
                var promotionRate = productMapOrder.Product.PromotionRate;
                var cost = price - price * promotionRate;

                productOrderMailContent += 
                    $" <tr style=\"font-size: 12px;\">" +
                    $" <td align=\"left\" style=\"padding: 3px 9px; font-family: Roboto, RobotoDraft, Helvetica, Arial, sans-serif; font-size: 12px;\" valign=\"top\">" +
                    $"<span class=\"m_-1741533549007004024name\" style=\"font-size: 12px;\">{name}</span></td>" +
                    $" <td align=\"left\" style=\"padding: 3px 9px; font-family: Roboto, RobotoDraft, Helvetica, Arial, sans-serif; font-size: 12px;\" valign=\"top\">{price}&nbsp;₫</td>" +
                    $" <td align=\"left\" style=\"padding: 3px 9px; font-family: Roboto, RobotoDraft, Helvetica, Arial, sans-serif; font-size: 12px;\" valign=\"top\">{amount}</td>" +
                    $" <td align=\"left\" style=\"padding: 3px 9px; font-family: Roboto, RobotoDraft, Helvetica, Arial, sans-serif; font-size: 12px;\" valign=\"top\">{promotionRate}&nbsp;₫</td>" +
                    $" <td align=\"right\" style=\"padding: 3px 9px; font-family: Roboto, RobotoDraft, Helvetica, Arial, sans-serif; font-size: 12px;\" valign=\"top\">{cost}&nbsp;₫</td> </tr>";
            }
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(EmailOption.EmailFrom, EmailOption.EmailFromName);
            var to = new EmailAddress(sender.ReceiverEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, sender.Subject, sender.PlainContent, sender.HtmlContent);
            msg.TemplateId = orderSuccessTemplateId;
            msg.AddSubstitutions(new Dictionary<string, string>
            {
                { "&lt;%subject%&gt;", "Thanh toán thành công" },
                { "&lt;%ORDERID%&gt;", productOrder.OrderId },
                { "&lt;%OrderDate%&gt;", productOrder.OrderDate.ToLongDateString() },
                { "&lt;%CustomerFullName%&gt;", productOrder.Customer.FullName },
                { "&lt;%CustomerEmail%&gt;", productOrder.Customer.Email },
                { "&lt;%CustomerAddress%&gt;", productOrder.Customer.Address },
                { "&lt;%CustomerPhone%&gt;", productOrder.Customer.Phone },
                { "&lt;%ProductOrder%&gt;", productOrderMailContent },
                { "&lt;%TotalCost%&gt;", productOrder.TotalCost.ToString() }
            });
            var response = await client.SendEmailAsync(msg);

            return new Result<bool>()
            {
                Succeed = true,
                Content = true
            };
        }
    }
}
