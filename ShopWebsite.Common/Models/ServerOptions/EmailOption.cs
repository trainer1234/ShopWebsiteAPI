using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.Common.Models.ServerOptions
{
    public static class EmailOption
    {
        public static string SendGridApiKey { get; set; }
        public static string OrderSuccessTemplateId { get; set; }
        public static string EmailFrom { get; set; }
        public static string EmailFromName { get; set; }
    }
}
