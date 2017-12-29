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
            _context.Add(newManufacture);
            _context.SaveChanges();

            return true;
        }

        public async Task<bool> Edit(Manufacture newManufacture)
        {
            var manufactureExist = await _context.Manufactures.Where(manufacture => manufacture.Id == newManufacture.Id)
                                            .Include(manufacture => manufacture.ManufactureTypes).Take(1).ToListAsync();

            if (manufactureExist != null && manufactureExist.Count > 0)
            {
                var searchManufacture = manufactureExist[0];
                searchManufacture.Name = newManufacture.Name;

                _context.Update(searchManufacture);
                _context.SaveChanges();

                return true;
            }
            return false;
        }

        public async Task<List<Manufacture>> GetAll()
        {
            var manufactures = await _context.Manufactures.Include(manufacture => manufacture.ManufactureTypes).ToListAsync();

            return manufactures;
        }

        public async Task<Manufacture> GetBy(string manufactureId)
        {
            var searchManufacture = await _context.Manufactures.Where(manufacture => manufacture.Id == manufactureId)
                                            .Include(manufacture => manufacture.ManufactureTypes).Take(1).ToListAsync();
            if (searchManufacture != null)
            {
                return searchManufacture[0];
            }
            return null;
        }

        public async Task<bool> Remove(string manufactureId)
        {
            var manufactureExist = await _context.Manufactures.Where(manufacture => manufacture.Id == manufactureId)
                                           .Include(manufacture => manufacture.ManufactureTypes).ToListAsync();
            if (manufactureExist != null && manufactureExist.Count > 0)
            {
                var searchManufacture = manufactureExist[0];

                _context.Remove(searchManufacture);
                _context.SaveChanges();

                return true;
            }
            return false;
        }
    }
}
