using ShopWebsite.DAL.Context;
using ShopWebsite.DAL.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Implementations
{
    public class ProductMapOrderDetailRepository : IProductMapOrderDetailRepository
    {
        private ShopWebsiteSqlContext _context;

        public ProductMapOrderDetailRepository(ShopWebsiteSqlContext context)
        {
            _context = context;
        }


    }
}
