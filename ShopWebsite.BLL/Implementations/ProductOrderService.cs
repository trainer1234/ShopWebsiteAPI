using ShopWebsite.BLL.Contracts;
using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.Common.Models.Enums;
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
        private IProductMapOrderDetailRepository _productMapOrderDetailRepository;
        private IProductRepository _productRepository;
        private IErrorLogRepository _errorLogRepository;
        private ICustomerRepository _customerRepository;

        public ProductOrderService(IProductOrderRepository productOrderRepository, IErrorLogRepository errorLogRepository,
            IProductMapOrderDetailRepository productMapOrderDetailRepository, IProductRepository productRepository,
            ICustomerRepository customerRepository)
        {
            _productOrderRepository = productOrderRepository;
            _errorLogRepository = errorLogRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _productMapOrderDetailRepository = productMapOrderDetailRepository;
        }

        public async Task<Result<bool>> AddProductOrder(ProductOrder newProductOrder)
        {
            var result = new Result<bool>();

            try
            {
                var customerId = await _customerRepository.Add(newProductOrder.Customer);

                newProductOrder.CustomerId = customerId;
                newProductOrder.Customer = null;
                newProductOrder.OrderStatus = OrderStatus.NotConfirmed;
                newProductOrder.OrderDate = DateTime.UtcNow;
                var productMapOrderDetailTmp = newProductOrder.ProductMapOrderDetails;
                newProductOrder.ProductMapOrderDetails = null;

                _productOrderRepository.Add(newProductOrder);

                if (productMapOrderDetailTmp != null && productMapOrderDetailTmp.Count > 0)
                {
                    newProductOrder.ProductMapOrderDetails = productMapOrderDetailTmp;
                    long totalCost = 0, totalAmount = 0;
                    foreach (var productMapOrder in newProductOrder.ProductMapOrderDetails)
                    {
                        var product = await _productRepository.GetBy(productMapOrder.ProductId);
                        totalCost += product.Price * productMapOrder.ProductAmount;
                        totalAmount += productMapOrder.ProductAmount;

                        await _productMapOrderDetailRepository.Add(productMapOrder);
                    }

                    newProductOrder.ProductTotalAmount = totalAmount;
                    newProductOrder.TotalCost = totalCost;

                    await _productOrderRepository.Edit(newProductOrder);
                }

                result.Content = result.Succeed = true;
            }
            catch(Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }

            return result;
        }

        public async Task<Result<bool>> EditProductOrder(ProductOrder newProductOrder)
        {
            var result = new Result<bool>();

            try
            {
                if(newProductOrder.OrderStatus == OrderStatus.Delivering)
                {
                    if (newProductOrder.ProductMapOrderDetails != null && newProductOrder.ProductMapOrderDetails.Count > 0)
                    {
                        foreach (var productMapOrder in newProductOrder.ProductMapOrderDetails)
                        {
                            var product = await _productRepository.GetBy(productMapOrder.ProductId);

                            product.Remain -= productMapOrder.ProductAmount;

                            await _productRepository.Edit(product);
                        }

                        newProductOrder.ProductMapOrderDetails = null;
                    }
                }
                var editResult = await _productOrderRepository.Edit(newProductOrder);
                if (editResult)
                {
                    result.Content = result.Succeed = true;
                }
                else
                {
                    result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(21, "No order");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }

            return result;
        }

        public async Task<Result<ProductOrder>> Get(string orderId)
        {
            var result = new Result<ProductOrder>();

            try
            {
                var productOrder = await _productOrderRepository.Get(orderId);
                if(productOrder != null)
                {
                    result.Content = productOrder;
                    result.Succeed = true;
                }
                else
                {
                    result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(21, "No order");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }

            return result;
        }

        public async Task<Result<List<ProductOrder>>> GetAll()
        {
            var result = new Result<List<ProductOrder>>();

            try
            {
                var productOrders = await _productOrderRepository.GetAll();
                if (productOrders != null)
                {
                    result.Content = productOrders;
                    result.Succeed = true;
                }
                else
                {
                    result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(22, "No orders");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }

            return result;
        }

        public async Task<Result<List<ProductOrder>>> FilterBy(OrderStatus orderStatus)
        {
            var result = new Result<List<ProductOrder>>();

            try
            {
                var productOrders = await _productOrderRepository.FilterBy(orderStatus);
                if (productOrders != null)
                {
                    result.Content = productOrders;
                    result.Succeed = true;
                }
                else
                {
                    result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(22, "No orders");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }

            return result;
        }

        public async Task<Result<bool>> RemoveProductOrder(string orderId)
        {
            var result = new Result<bool>();

            try
            {
                await _productMapOrderDetailRepository.RemoveByProductOrderDetailId(orderId);
                var removeResult = await _productOrderRepository.Remove(orderId);
                if (removeResult)
                {
                    result.Content = removeResult;
                    result.Succeed = true;
                }
                else
                {
                    result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(21, "No order");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }

            return result;
        }
    }
}
