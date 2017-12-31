using ShopWebsite.DAL.Models.ProductOrderModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.DAL.Contracts
{
    public interface IProductOrderRepository
    {
        void Add(ProductOrder newProductOrder);
        Task<bool> Edit(ProductOrder newProductOrder);
        Task<bool> Remove(string productOrderId);
        Task<ProductOrder> Get(string productOrderId);
        Task<List<ProductOrder>> GetAll();
    }
}
