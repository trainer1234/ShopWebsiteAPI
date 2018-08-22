using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.Common.Models.EmailModels.SendGrid;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.BLL.Contracts
{
    public interface IEmailService
    {
        Task<Result<bool>> SendEmail(EmailSender sender, string apiKey);
    }
}
