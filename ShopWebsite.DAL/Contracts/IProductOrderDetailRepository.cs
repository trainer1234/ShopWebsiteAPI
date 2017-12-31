using ShopWebsite.DAL.Models.ProductOrderModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.DAL.Contracts
{
    public interface IProductOrderDetailRepository
    {
        void Add(ProductOrderDetail newProductOrderDetail);
        Task<bool> Remove(string productOrderId);
        Task<ProductOrderDetail> Get(string productOrderId);
        Task<List<ProductOrderDetail>> GetAll();
    }
}
