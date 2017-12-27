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
    public class ProductSpecificTypeRepository : IProductSpecificTypeRepository
    {
        private ShopWebsiteSqlContext _context;

        public ProductSpecificTypeRepository(ShopWebsiteSqlContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(ProductSpecificType newProductSpecificType)
        {
            var productSpecificTypeExist = await _context.ProductSpecificTypes.Where(type => type.Name == newProductSpecificType.Name).ToListAsync();
            if(productSpecificTypeExist != null && productSpecificTypeExist.Count > 0)
            {
                return false;
            }
            else
            {
                _context.Add(newProductSpecificType);
                _context.SaveChanges();

                return true;
            }
        }

        public async Task<bool> Edit(ProductSpecificType newProductSpecificType)
        {
            var productSpecificTypeExist = await _context.ProductSpecificTypes.Where(type => type.Id == newProductSpecificType.Id).ToListAsync();
            if (productSpecificTypeExist != null && productSpecificTypeExist.Count > 0)
            {
                var searchProductSpecificType = productSpecificTypeExist[0];
                searchProductSpecificType.Name = newProductSpecificType.Name;
                searchProductSpecificType.Type = newProductSpecificType.Type;

                _context.Update(searchProductSpecificType);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<ProductSpecificType>> GetAll()
        {
            var productSpecificTypes = await _context.ProductSpecificTypes.ToListAsync();
            return productSpecificTypes;
        }

        public async Task<ProductSpecificType> GetBy(string productSpecificTypeId)
        {
            var productSpecificTypes = await _context.ProductSpecificTypes.Where(type => type.Id == productSpecificTypeId).ToListAsync();
            if(productSpecificTypes != null && productSpecificTypes.Count > 0)
            {
                var searchType = productSpecificTypes[0];
                return searchType;
            }
            return null;
        }

        public async Task<bool> Remove(string productSpecificTypeId)
        {
            var productSpecificTypes = await _context.ProductSpecificTypes.Where(type => type.Id == productSpecificTypeId).ToListAsync();
            if (productSpecificTypes != null && productSpecificTypes.Count > 0)
            {
                var searchType = productSpecificTypes[0];

                _context.Remove(searchType);
                _context.SaveChanges();

                return true;
            }
            return false;
        }
    }
}
