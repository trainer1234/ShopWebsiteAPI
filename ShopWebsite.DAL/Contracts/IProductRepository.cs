using ShopWebsite.DAL.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.DAL.Contracts
{
    public interface IProductRepository
    {
        Task<bool> Add(Product newProduct);
        Task<bool> Edit(Product newProduct);
        Task<bool> Remove(string productId);
        Task<Product> GetBy(string productId);
        Task<List<Product>> GetProductBy(string type, int num);
        //Task<List<Product>> FilterProductBy(string specificTypeId, int num);
        Task<List<Product>> GetAll();
    }
}
