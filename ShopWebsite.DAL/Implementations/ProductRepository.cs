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
    public class ProductRepository : IProductRepository
    {
        private ShopWebsiteSqlContext _context;

        public ProductRepository(ShopWebsiteSqlContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Product newProduct)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Edit(Product newProduct)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetBy(string productId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetProductBy(string type, int num)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Remove(string productId)
        {
            throw new NotImplementedException();
        }
    }
}
