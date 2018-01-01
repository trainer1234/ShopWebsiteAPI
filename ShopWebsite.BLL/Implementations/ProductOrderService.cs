using ShopWebsite.BLL.Contracts;
using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.DAL.Contracts;
using ShopWebsite.DAL.Models.ProductOrderModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.BLL.Implementations
{
    public class ProductOrderService : IProductOrderService
    {
        private IProductOrderRepository _productOrderRepository;
        private IErrorLogRepository _errorLogRepository;

        public ProductOrderService(IProductOrderRepository productOrderRepository, IErrorLogRepository errorLogRepository)
        {
            _productOrderRepository = productOrderRepository;
            _errorLogRepository = errorLogRepository;
        }

        public async Task<Result<bool>> AddProductOrder(ProductOrder newProductOrder)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<bool>> EditProductOrder(ProductOrder newProductOrder)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<ProductOrder>> Get(string orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<ProductOrder>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<bool>> RemoveProductOrder(string orderId)
        {
            throw new NotImplementedException();
        }
    }
}
