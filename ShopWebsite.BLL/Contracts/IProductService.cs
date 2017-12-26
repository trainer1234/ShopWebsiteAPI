using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.DAL.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.BLL.Contracts
{
    public interface IProductService
    {
        Task<Result<bool>> AddProduct(Product newProduct);
        Task<Result<bool>> EditProduct(Product newProduct);
        Task<Result<bool>> RemoveProduct(string productId);
        Task<Result<List<Product>>> GetAllProduct();
        Task<Result<List<Product>>> GetRecentProductBy(string type, int num);
        Task<Result<Product>> GetProductBy(string productId);
    }
}
