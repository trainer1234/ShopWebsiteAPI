using Microsoft.EntityFrameworkCore;
using ShopWebsite.DAL.Context;
using ShopWebsite.DAL.Contracts;
using ShopWebsite.DAL.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.DAL.Implementations
{
    public class ProductPropertyRepository : IProductPropertyRepository
    {
        private ShopWebsiteSqlContext _context;

        public ProductPropertyRepository(ShopWebsiteSqlContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(ProductProperty newProductProperty)
        {
            var productPropertyExist = await _context.ProductProperties.Where(productProp =>
                    (productProp.ProductId == newProductProperty.ProductId && productProp.PropertyId == newProductProperty.PropertyId)).ToListAsync();
            if(productPropertyExist != null && productPropertyExist.Count > 0)
            {
                return false;
            }
            else
            {
                _context.Add(newProductProperty);
                _context.SaveChanges();

                return true;
            }
        }

        public async Task<bool> Remove(string productId, string propertyId)
        {
            var productPropertyExist = await _context.ProductProperties.Where(productProp =>
                    (productProp.ProductId == productId && productProp.PropertyId == propertyId)).ToListAsync();
            if (productPropertyExist != null && productPropertyExist.Count > 0)
            {
                var searchProductProp = productPropertyExist[0];

                _context.Remove(searchProductProp);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> RemoveByProductId(string productId)
        {
            var productPropertyExist = await _context.ProductProperties.Where(productProp => productProp.ProductId == productId).ToListAsync();
            if (productPropertyExist != null && productPropertyExist.Count > 0)
            {
                foreach (var productProp in productPropertyExist)
                {
                    _context.Remove(productProp);
                }
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> RemoveByPropertyId(string propertyId)
        {
            var productPropertyExist = await _context.ProductProperties.Where(productProp => productProp.PropertyId == propertyId).ToListAsync();
            if (productPropertyExist != null && productPropertyExist.Count > 0)
            {
                foreach (var productProp in productPropertyExist)
                {
                    _context.Remove(productProp);
                }
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
