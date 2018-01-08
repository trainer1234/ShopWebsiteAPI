using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.Common.Models.Enums;
using ShopWebsite.DAL.Models.ProductOrderModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.BLL.Contracts
{
    public interface IProductOrderService
    {
        Task<Result<string>> AddProductOrder(ProductOrder newProductOrder);
        Task<Result<bool>> EditProductOrder(ProductOrder newProductOrder);
        Task<Result<bool>> RemoveProductOrder(string orderId);
        Task<Result<ProductOrder>> Get(string orderId);
        Task<Result<List<ProductOrder>>> FilterBy(OrderStatus orderStatus);
        Task<Result<List<ProductOrder>>> GetAll();
    }
}
