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
                                        .Include(product => product.ProductProperties)
                                            .ThenInclude(property => property.Property).Take(1).ToListAsync();
            if (productExist != null && productExist.Count > 0)
            {
                var searchProduct = productExist[0];

                searchProduct.ManufactureYear = newProduct.ManufactureYear;
                searchProduct.ManufactureId = newProduct.ManufactureId;
                searchProduct.PromotionRate = newProduct.PromotionRate;
                searchProduct.Name = newProduct.Name;
                searchProduct.Price = newProduct.Price;
                searchProduct.ProductSpecificType = newProduct.ProductSpecificType;
                searchProduct.Type = newProduct.Type;
                searchProduct.Remain = newProduct.Remain;
                searchProduct.Detail = newProduct.Detail;
                searchProduct.View = newProduct.View;
                searchProduct.PurchaseCounter = newProduct.PurchaseCounter;

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
                                        .Include(product => product.ProductProperties)
                                            .ThenInclude(property => property.Property).ToListAsync();

            return products;
        }

        public async Task<List<Product>> GetAllBy(ProductType type)
        {
            var products = new List<Product>();
            if(type == ProductType.Discount)
            {
                products = await _context.Products.Where(product => product.PromotionRate > 0)
                                    .Include(product => product.ProductImages)
                                    .Include(product => product.Manufacture)
                                        .ThenInclude(manufacture => manufacture.ManufactureTypes)
                                    .Include(product => product.ProductProperties)
                                        .ThenInclude(property => property.Property).ToListAsync();
            }
            else
            {
                products = await _context.Products.Where(product => product.Type == type)
                                    .Include(product => product.ProductImages)
                                    .Include(product => product.Manufacture)
                                        .ThenInclude(manufacture => manufacture.ManufactureTypes)
                                    .Include(product => product.ProductProperties)
                                        .ThenInclude(property => property.Property).ToListAsync();
            }

            return products;
        }

        public List<Product> GetTopNProductByView(int n)
        {
            var products = _context.Products.Include(product => product.ProductImages)
                                    .Include(product => product.Manufacture)
                                        .ThenInclude(manufacture => manufacture.ManufactureTypes)
                                    .Include(product => product.ProductProperties)
                                        .ThenInclude(property => property.Property)
                                    .OrderBy(product => product.View).Take(n).ToList();

            return products;
        }

        public List<Product> GetTopNProductByPurchaseCounter(int n)
        {
            var products = _context.Products.Include(product => product.ProductImages)
                                    .Include(product => product.Manufacture)
                                        .ThenInclude(manufacture => manufacture.ManufactureTypes)
                                    .Include(product => product.ProductProperties)
                                        .ThenInclude(property => property.Property)
                                    .OrderBy(product => product.PurchaseCounter).Take(n).ToList();

            return products;
        }

        public async Task<Product> GetBy(string productId)
        {
            var products = await _context.Products.Where(product => product.Id == productId)
                                    .Include(product => product.ProductImages)
                                    .Include(product => product.Manufacture)
                                        .ThenInclude(manufacture => manufacture.ManufactureTypes)
                                    .Include(product => product.ProductProperties)
                                        .ThenInclude(property => property.Property).ToListAsync();
            if (products != null && products.Count > 0)
            {
                return products[0];
            }
            return null;
        }

        public async Task<bool> IncreaseRemain(string productId, long amount)
        {
            var products = await _context.Products.Where(product => product.Id == productId).ToListAsync();
            if (products != null && products.Count > 0)
            {
                var product = products[0];
                product.Remain += amount;

                _context.Update(product);
                _context.SaveChanges();

                return true;
            }
            return false;
        }

        public async Task<List<Product>> GetProductBy(ProductType type, int num)
        {
            var products = new List<Product>();
            if(type == ProductType.Discount)
            {
                products = await _context.Products.Where(product => product.PromotionRate > 0)
                                    .Include(product => product.ProductImages)
                                    .Include(product => product.Manufacture)
                                        .ThenInclude(manufacture => manufacture.ManufactureTypes)
                                    .Include(product => product.ProductProperties)
                                        .ThenInclude(property => property.Property).Take(num).ToListAsync();
            }
            else
            {
                products = await _context.Products.Where(product => product.Type == type)
                                    .Include(product => product.ProductImages)
                                    .Include(product => product.Manufacture)
                                        .ThenInclude(manufacture => manufacture.ManufactureTypes)
                                    .Include(product => product.ProductProperties)
                                        .ThenInclude(property => property.Property).Take(num).ToListAsync();
            }

            return products;
        }

        public async Task<List<Product>> Search(string key)
        {
            var products = await _context.Products.Include(product => product.ProductImages)
                                    .Include(product => product.Manufacture)
                                        .ThenInclude(manufacture => manufacture.ManufactureTypes)
                                    .Include(product => product.ProductProperties)
                                        .ThenInclude(property => property.Property).ToListAsync();
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
                                    .Include(product => product.ProductProperties)
                                        .ThenInclude(property => property.Property).ToListAsync();

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
                                    .Include(product => product.ProductProperties)
                                        .ThenInclude(property => property.Property).ToListAsync();

            if (products != null && products.Count > 0)
            {
                foreach (var product in products)
                {
                    _context.Remove(product);
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
