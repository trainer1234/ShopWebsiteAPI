using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.Common.Models.EmailModels.SendGrid
{
    public class EmailSender
    {
        public string ApiKey { get; set; }
        public string SenderEmail { get; set; }
        public string ReceiverEmail { get; set; }
        public string Subject { get; set; }
        public string PlainContent { get; set; }
        public string HtmlContent { get; set; }
        public string TemplateId { get; set; }
    }
}
