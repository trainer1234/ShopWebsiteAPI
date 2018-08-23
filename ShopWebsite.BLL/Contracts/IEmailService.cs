using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.Common.Models.EmailModels.SendGrid;
using ShopWebsite.DAL.Models.ProductOrderModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.BLL.Contracts
{
    public interface IEmailService
    {
        Task<Result<bool>> SendEmail(EmailSender sender, string apiKey);
        Task<Result<bool>> SendOrderSuccessEmail(ProductOrder productOrder, EmailSender sender, string apiKey, string orderSuccessTemplateId);
    }
}
