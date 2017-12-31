using ShopWebsite.DAL.Context;
using ShopWebsite.DAL.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Implementations
{
    public class ProductOrderDetailRepository : IProductOrderDetailRepository
    {
        private ShopWebsiteSqlContext _context;

        public ProductOrderDetailRepository(ShopWebsiteSqlContext context)
        {
            _context = context;
        }


    }
}
