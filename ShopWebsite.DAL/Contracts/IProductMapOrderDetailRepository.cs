using ShopWebsite.DAL.Models.ProductOrderModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.DAL.Contracts
{
    public interface IProductMapOrderDetailRepository
    {
        Task<bool> Add(ProductMapOrderDetail newProductMapOrderDetail);
        Task<bool> Remove(string productOrderDetailId, string productId);
        Task<bool> RemoveByProductId(string productId);
        Task<bool> RemoveByProductOrderDetailId(string productOrderDetailId);
    }
}
