using ShopWebsite.DAL.Context;
using ShopWebsite.DAL.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Implementations
{
    public class ProductImageRepository : IProductImageRepository
    {
        private ShopWebsiteSqlContext _context;

        public ProductImageRepository(ShopWebsiteSqlContext context)
        {
            _context = context;
        }


    }
}
