using Microsoft.EntityFrameworkCore;
using ShopWebsite.DAL.Context;
using ShopWebsite.DAL.Contracts;
using ShopWebsite.DAL.Models.PropertyModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.DAL.Implementations
{
    public class PropertyRepository : IPropertyRepository
    {
        private ShopWebsiteSqlContext _context;

        public PropertyRepository(ShopWebsiteSqlContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Property newProperty)
        {
            var propertyExist = await _context.Properties.Where(property => property.Name == newProperty.Name).Take(1).ToListAsync();
            if(propertyExist != null && propertyExist.Count > 0)
            {
                return false;
            }
            else
            {
                _context.Add(newProperty);
                _context.SaveChanges();

                return true;
            }
        }

        public async Task<bool> Edit(Property newProperty)
        {
            var propertyExist = await _context.Properties.Where(property => property.Id == newProperty.Id).Take(1).ToListAsync();
            if (propertyExist != null && propertyExist.Count > 0)
            {
                var searchProperty = propertyExist[0];
                searchProperty.Name = newProperty.Name;

                _context.Update(searchProperty);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Property>> GetAll()
        {
            var properties = await _context.Properties.ToListAsync();

            return properties;
        }

        public async Task<Property> GetBy(string propertyId)
        {
            var propertyExist = await _context.Properties.Where(property => property.Id == propertyId).Take(1).ToListAsync();
            if (propertyExist != null && propertyExist.Count > 0)
            {
                var searchProperty = propertyExist[0];

                return searchProperty;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> Remove(string propertyId)
        {
            var propertyExist = await _context.Properties.Where(property => property.Id == propertyId).Take(1).ToListAsync();
            if (propertyExist != null && propertyExist.Count > 0)
            {
                var searchProperty = propertyExist[0];

                _context.Remove(searchProperty);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
