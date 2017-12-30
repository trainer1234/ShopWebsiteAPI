using Microsoft.EntityFrameworkCore;
using ShopWebsite.Common.Models.Enums;
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
    public class ManufactureTypeRepository : IManufactureTypeRepository
    {
        private ShopWebsiteSqlContext _context;

        public ManufactureTypeRepository(ShopWebsiteSqlContext context)
        {
            _context = context;
        }

        public async Task<HashSet<Manufacture>> FilterBy(ProductType productType)
        {
            var manufactureTypes = await _context.ManufactureTypes.Where(manufactureType => manufactureType.Type == productType)
                                            .Include(type => type.Manufacture).ToListAsync();
            if(manufactureTypes != null && manufactureTypes.Count > 0)
            {
                var manufactures = new HashSet<Manufacture>();
                foreach (var type in manufactureTypes)
                {
                    manufactures.Add(type.Manufacture);
                }

                return manufactures;
            }
            return null;
        }

        public async Task<bool> Add(ManufactureType newManufactureType)
        {
            var manufactureTypeExist = await _context.ManufactureTypes.Where(manufactureType =>
                (manufactureType.ManufactureId == newManufactureType.ManufactureId && manufactureType.Type == newManufactureType.Type)).ToListAsync();
            if(manufactureTypeExist != null && manufactureTypeExist.Count > 0)
            {
                return false;
            }
            else
            {
                _context.Add(newManufactureType);
                _context.SaveChanges();

                return true;
            }
        }

        public async Task<bool> Remove(string manufactureId)
        {
            var manufactureTypes = await _context.ManufactureTypes.Where(manufactureType => manufactureType.ManufactureId == manufactureId).ToListAsync();
            if (manufactureTypes != null && manufactureTypes.Count > 0)
            {
                foreach (var manufactureType in manufactureTypes)
                {
                    _context.Remove(manufactureType);
                    _context.SaveChanges();
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
