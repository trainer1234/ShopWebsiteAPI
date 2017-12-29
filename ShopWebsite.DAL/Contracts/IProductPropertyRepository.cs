using ShopWebsite.DAL.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.DAL.Contracts
{
    public interface IProductPropertyRepository
    {
        Task<bool> Add(ProductProperty newProductProperty);
        Task<bool> Remove(string productId, string propertyId);
        Task<bool> RemoveByProductId(string productId);
        Task<bool> RemoveByPropertyId(string propertyId);
    }
}
