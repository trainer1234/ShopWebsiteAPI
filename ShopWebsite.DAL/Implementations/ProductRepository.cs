using Microsoft.EntityFrameworkCore;
using ShopWebsite.Common.Models.Enums;
using ShopWebsite.DAL.Context;
using ShopWebsite.DAL.Contracts;
using ShopWebsite.DAL.Models.ProductModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.DAL.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private ShopWebsiteSqlContext _context;

        public ProductRepository(ShopWebsiteSqlContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Product newProduct)
        {
            _context.Add(newProduct);
            _context.SaveChanges();

            return true;
        }

        public async Task<bool> Edit(Product newProduct)
        {
            var productExist = await _context.Products.Where(product => product.Id == newProduct.Id).Take(1).ToListAsync();
            if(productExist != null && productExist.Count > 0)
            {
                var searchProduct = productExist[0];
                searchProduct.ManufactureYear = newProduct.ManufactureYear;
                searchProduct.Name = newProduct.Name;
                searchProduct.Price = newProduct.Price;
                searchProduct.ProductSpecificType = newProduct.ProductSpecificType;
                searchProduct.Type = newProduct.Type;

                _context.Update(searchProduct);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Product>> GetAll()
        {
            var products = await _context.Products.ToListAsync();

            return products;
        }

        public async Task<List<Product>> GetAllBy(ProductType type)
        {
            var products = await _context.Products.Where(product => product.Type == type).ToListAsync();

            return products;
        }

        public async Task<Product> GetBy(string productId)
        {
            var products = await _context.Products.Where(product => product.Id == productId).ToListAsync();
            if(products != null && products.Count > 0)
            {
                return products[0];
            }
            return null;
        }

        public async Task<List<Product>> GetProductBy(ProductType type, int num)
        {
            var products = await _context.Products.Where(product => product.Type == type).Take(num).ToListAsync();

            return products;
        }

        public async Task<bool> Remove(string productId)
        {
            var products = await _context.Products.Where(product => product.Id == productId).ToListAsync();

            if(products != null && products.Count > 0)
            {
                var searchProduct = products[0];

                _context.Remove(searchProduct);
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
