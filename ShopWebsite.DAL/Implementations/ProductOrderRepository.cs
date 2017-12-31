using ShopWebsite.DAL.Context;
using ShopWebsite.DAL.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Implementations
{
    public class ProductOrderRepository : IProductOrderRepository
    {
        private ShopWebsiteSqlContext _context;

        public ProductOrderRepository(ShopWebsiteSqlContext context)
        {
            _context = context;
        }


    }
}
