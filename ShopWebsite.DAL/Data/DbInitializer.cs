using ShopWebsite.DAL.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ShopWebsiteSqlContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
