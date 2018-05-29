using ShopWebsite.Common.Models.Enums;
using ShopWebsite.DAL.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.DAL.Contracts
{
    public interface IProductRepository
    {
        Task<string> Add(Product newProduct);
        Task<bool> Edit(Product newProduct);
        Task<bool> Remove(string productId);
        Task<bool> IncreaseRemain(string productId, long amount);
        Task<bool> RemoveProductBy(string manufactureId);
        Task<Product> GetBy(string productId);
        Task<List<Product>> GetProductBy(ProductType type, int num);
        Task<List<Product>> GetAllBy(ProductType type);
        Task<List<Product>> GetAll();
        List<Product> GetTopNProductByView(int n);
        List<Product> GetTopNProductByPurchaseCounter(int n);
        Task<List<Product>> Search(string key);
    }
}
