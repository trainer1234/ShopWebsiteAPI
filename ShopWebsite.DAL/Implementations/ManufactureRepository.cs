using Microsoft.EntityFrameworkCore;
using ShopWebsite.DAL.Context;
using ShopWebsite.DAL.Contracts;
using ShopWebsite.DAL.Models.ManufactureModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.DAL.Implementations
{
    public class ManufactureRepository : IManufactureRepository
    {
        private ShopWebsiteSqlContext _context;

        public ManufactureRepository(ShopWebsiteSqlContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Manufacture newManufacture)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Edit(Manufacture newManufacture)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Manufacture>> GetAll()
        {
            var manufactures = await _context.Manufactures.ToListAsync();

            return manufactures;
        }

        public async Task<Manufacture> GetBy(string manufactureId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Remove(string manufactureId)
        {
            throw new NotImplementedException();
        }
    }
}
