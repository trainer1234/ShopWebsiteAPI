using ShopWebsite.Common.Models.Enums;
using ShopWebsite.DAL.Models.ProductOrderModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.DAL.Contracts
{
    public interface IProductOrderRepository
    {
        string Add(ProductOrder newProductOrder);
        Task<bool> Edit(ProductOrder newProductOrder);
        Task<bool> Remove(string productOrderId);
        Task<ProductOrder> Get(string productOrderId);
        Task<List<ProductOrder>> FilterBy(OrderStatus orderStatus);
        Task<List<ProductOrder>> GetAll();
    }
}
