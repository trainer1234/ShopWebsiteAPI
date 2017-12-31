using ShopWebsite.DAL.Context;
using ShopWebsite.DAL.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        private ShopWebsiteSqlContext _context;

        public CustomerRepository(ShopWebsiteSqlContext context)
        {
            _context = context;
        }


    }
}
