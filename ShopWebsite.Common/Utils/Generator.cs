using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.Common.Utils
{
    public static class Generator
    {
        public static string GenerateOrderId(int length)
        {
            string result = "";
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                result += chars[rnd.Next(chars.Length)];
            }
            return result;
        }
    }
}
