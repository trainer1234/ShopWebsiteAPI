using ShopWebsite.BLL.Contracts;
using ShopWebsite.DAL.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

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


    }
}
