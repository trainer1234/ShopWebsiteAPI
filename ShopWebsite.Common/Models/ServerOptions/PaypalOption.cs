using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.Common.Models.ServerOptions
{
    public static class PaypalOption
    {
        public static string ClientId { get; set; }
        public static string ClientSecret { get; set; }
        public static string PayeeEmail { get; set; }
        public static string PayeeMerchantId { get; set; }
        public static string ReturnUrl { get; set; }
        public static string CancelUrl { get; set; }
        public static string TransactionDescription { get; set; }
    }
}
