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
            var productExist = await _context.Products.Where(product => product.Id == newProduct.Id)
                                        .Include(product => product.ProductImages)
                                        .Include(product => product.Manufacture)
                                            .ThenInclude(manufacture => manufacture.ManufactureTypes)
                                        .Include(product => product.ProductProperties).Take(1).ToListAsync();
            if (productExist != null && productExist.Count > 0)
            {
                var searchProduct = productExist[0];
                searchProduct.ManufactureYear = newProduct.ManufactureYear;
                searchProduct.ManufactureId = newProduct.ManufactureId;
                searchProduct.PromotionAvailable = newProduct.PromotionAvailable;
                searchProduct.PromotionRate = newProduct.PromotionRate;
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
            var products = await _context.Products.Include(product => product.ProductImages)
                                        .Include(product => product.Manufacture)
                                            .ThenInclude(manufacture => manufacture.ManufactureTypes)
                                        .Include(product => product.ProductProperties).ToListAsync();

            return products;
        }

        public async Task<List<Product>> GetAllBy(ProductType type)
        {
            var products = await _context.Products.Where(product => product.Type == type)
                                    .Include(product => product.ProductImages)
                                    .Include(product => product.Manufacture)
                                    .Include(product => product.ProductProperties).ToListAsync();

            return products;
        }

        public async Task<Product> GetBy(string productId)
        {
            var products = await _context.Products.Where(product => product.Id == productId)
                                    .Include(product => product.ProductImages)
                                    .Include(product => product.Manufacture)
                                        .ThenInclude(manufacture => manufacture.ManufactureTypes)
                                    .Include(product => product.ProductProperties).ToListAsync();
            if (products != null && products.Count > 0)
            {
                return products[0];
            }
            return null;
        }

        public async Task<List<Product>> GetProductBy(ProductType type, int num)
        {
            var products = await _context.Products.Where(product => product.Type == type)
                                    .Include(product => product.ProductImages)
                                    .Include(product => product.Manufacture)
                                        .ThenInclude(manufacture => manufacture.ManufactureTypes)
                                    .Include(product => product.ProductProperties).Take(num).ToListAsync();

            return products;
        }

        public async Task<List<Product>> Search(string key)
        {
            var products = await _context.Products.Include(product => product.ProductImages)
                                    .Include(product => product.Manufacture)
                                        .ThenInclude(manufacture => manufacture.ManufactureTypes)
                                    .Include(product => product.ProductProperties).ToListAsync();
            products = products.Where(product => (product.Name.Trim().ToLower().Contains(key.Trim().ToLower()) 
                                        || product.Manufacture.Name.Trim().ToLower().Contains(key.Trim().ToLower()))).ToList();
            return products;
        }

        public async Task<bool> Remove(string productId)
        {
            var products = await _context.Products.Where(product => product.Id == productId)
                                    .Include(product => product.ProductImages)
                                    .Include(product => product.Manufacture)
                                        .ThenInclude(manufacture => manufacture.ManufactureTypes)
                                    .Include(product => product.ProductProperties).ToListAsync();

            if (products != null && products.Count > 0)
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

        public async Task<bool> RemoveProductBy(string manufactureId)
        {
            var products = await _context.Products.Where(product => product.ManufactureId == manufactureId)
                                    .Include(product => product.ProductImages)
                                    .Include(product => product.Manufacture)
                                        .ThenInclude(manufacture => manufacture.ManufactureTypes)
                                    .Include(product => product.ProductProperties).ToListAsync();

            if (products != null && products.Count > 0)
            {
                foreach (var product in products)
                {
                    _context.Remove(product);
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
