using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.Common.Models.Enums;
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
        Task<Result<bool>> IncreaseRemain(string productId, long amount);
        Task<Result<List<Product>>> GetAllProduct();
        Task<Result<List<Product>>> GetRecentProductBy(ProductType type, int num);
        Task<Result<List<Product>>> GetAllProductBy(ProductType type);
        Result<List<Product>> GetTopNProductByView(int n);
        Result<List<Product>> GetTopNProductByPurchaseCounter(int n);
        Task<Result<Product>> GetProductBy(string productId);
        Task<Result<List<Product>>> SearchProduct(string key);
    }
}
