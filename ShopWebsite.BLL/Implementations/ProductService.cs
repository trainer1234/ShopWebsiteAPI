using ShopWebsite.BLL.Contracts;
using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.DAL.Contracts;
using ShopWebsite.DAL.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopWebsite.BLL.Implementations
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<bool>> AddProduct(Product newProduct)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<bool>> EditProduct(Product newProduct)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<Product>>> GetAllProduct()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Product>> GetProductBy(string productId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<Product>>> GetRecentProductBy(string type, int num)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<bool>> RemoveProduct(string productId)
        {
            throw new NotImplementedException();
        }
    }
}
