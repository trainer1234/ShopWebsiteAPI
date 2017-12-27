using ShopWebsite.DAL.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.DAL.Contracts
{
    public interface IProductImageRepository
    {
        Task<bool> Add(ProductImage newProductImage);
        Task<bool> Remove(string productId, string imageModelId);
        Task<bool> RemoveByProductId(string productId);
        Task<bool> RemoveByImageModelId(string imageModelId);
    }
}
