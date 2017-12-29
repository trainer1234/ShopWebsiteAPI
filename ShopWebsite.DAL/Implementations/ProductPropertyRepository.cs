using ShopWebsite.DAL.Context;
using ShopWebsite.DAL.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Implementations
{
    public class ProductPropertyRepository : IProductPropertyRepository
    {
        private ShopWebsiteSqlContext _context;

        public ProductPropertyRepository(ShopWebsiteSqlContext context)
        {
            _context = context;
        }


    }
}
