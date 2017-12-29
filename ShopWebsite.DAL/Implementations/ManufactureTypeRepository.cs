using ShopWebsite.DAL.Context;
using ShopWebsite.DAL.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Implementations
{
    public class ManufactureTypeRepository : IManufactureTypeRepository
    {
        private ShopWebsiteSqlContext _context;

        public ManufactureTypeRepository(ShopWebsiteSqlContext context)
        {
            _context = context;
        }


    }
}
