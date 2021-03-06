﻿using Newtonsoft.Json;
using PayPal.Api;
using ShopWebsite.BLL.Contracts;
using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.Common.Models.CurrencyConverterApi;
using ShopWebsite.Common.Models.EmailModels.SendGrid;
using ShopWebsite.Common.Models.Enums;
using ShopWebsite.Common.Models.ServerOptions;
using ShopWebsite.Common.Utils;
using ShopWebsite.DAL.Contracts;
using ShopWebsite.DAL.Models.CustomerModels;
using ShopWebsite.DAL.Models.ProductModels;
using ShopWebsite.DAL.Models.ProductOrderModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
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

        private IPaypalService _paypalService;
        private IEmailService _emailService;

        public ProductOrderService(IProductOrderRepository productOrderRepository, IErrorLogRepository errorLogRepository,
            IProductMapOrderDetailRepository productMapOrderDetailRepository, IProductRepository productRepository,
            ICustomerRepository customerRepository, IPaypalService paypalService, IEmailService emailService)
        {
            _productOrderRepository = productOrderRepository;
            _errorLogRepository = errorLogRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _productMapOrderDetailRepository = productMapOrderDetailRepository;
            _paypalService = paypalService;
            _emailService = emailService;
        }

        public async Task<Result<ProductOrderPaypalResult>> AddProductOrder(ProductOrder newProductOrder, PaymentMethod paymentMethod)
        {
            var result = new Result<ProductOrderPaypalResult>();
            var productOrderPaypalResult = new ProductOrderPaypalResult();

            try
            {
                var customerId = await _customerRepository.Add(newProductOrder.Customer);

                newProductOrder.CustomerId = customerId;
                newProductOrder.Customer = null;
                newProductOrder.OrderStatus = OrderStatus.NotConfirmed;
                newProductOrder.OrderDate = DateTime.UtcNow;
                var productMapOrderDetailTmp = newProductOrder.ProductMapOrderDetails;
                newProductOrder.ProductMapOrderDetails = null;

                var orderId = _productOrderRepository.Add(newProductOrder);

                if (paymentMethod == PaymentMethod.Direct)
                {
                    if (productMapOrderDetailTmp != null && productMapOrderDetailTmp.Count > 0)
                    {
                        newProductOrder.ProductMapOrderDetails = productMapOrderDetailTmp;
                        double totalCost = 0;
                        long totalAmount = 0;
                        foreach (var productMapOrder in newProductOrder.ProductMapOrderDetails)
                        {
                            var product = await _productRepository.GetBy(productMapOrder.ProductId);
                            totalCost += (product.Price - (product.Price * product.PromotionRate) / 100) * productMapOrder.ProductAmount;
                            totalAmount += productMapOrder.ProductAmount;

                            product.PurchaseCounter += productMapOrder.ProductAmount;
                            await _productRepository.Edit(product);

                            await _productMapOrderDetailRepository.Add(productMapOrder);
                        }

                        newProductOrder.ProductTotalAmount = totalAmount;
                        newProductOrder.TotalCost = totalCost;

                        await _productOrderRepository.Edit(newProductOrder);
                    }

                    productOrderPaypalResult.OrderId = orderId;

                    EmailSender sender = new EmailSender
                    {
                        ReceiverEmail = newProductOrder.Customer.Email
                    };
                    var emailResponse = await _emailService.SendOrderSuccessEmail(newProductOrder, sender, EmailOption.SendGridApiKey, EmailOption.OrderSuccessTemplateId);

                    result.Content = productOrderPaypalResult;
                    result.Succeed = true;
                }
                else if (paymentMethod == PaymentMethod.Paypal)
                {
                    double exchangeRate = 0;
                    using (var client = new HttpClient())
                    {
                        var uri = "http://free.currencyconverterapi.com/api/v5/convert?q=VND_USD&compact=y";
                        var response = await client.GetAsync(uri);
                        response.EnsureSuccessStatusCode();
                        var jsonStringResult = await response.Content.ReadAsStringAsync();
                        var converter = JsonConvert.DeserializeObject<CurrencyConverter>(jsonStringResult);
                        if (converter.VND_USD != null)
                        {
                            exchangeRate = converter.VND_USD.val;
                        }
                    }

                    if (productMapOrderDetailTmp != null && productMapOrderDetailTmp.Count > 0)
                    {
                        newProductOrder.ProductMapOrderDetails = productMapOrderDetailTmp;
                        double totalCostInUSD = 0;
                        double totalCostInVND = 0;
                        long totalAmount = 0;
                        foreach (var productMapOrder in newProductOrder.ProductMapOrderDetails)
                        {
                            var product = await _productRepository.GetBy(productMapOrder.ProductId);
                            totalCostInVND += (product.Price - (product.Price * product.PromotionRate) / 100) * productMapOrder.ProductAmount;
                            totalCostInUSD += Math.Round((product.Price * exchangeRate - (product.Price * product.PromotionRate * exchangeRate) / 100) * productMapOrder.ProductAmount, 2);
                            totalAmount += productMapOrder.ProductAmount;

                            product.PurchaseCounter += productMapOrder.ProductAmount;
                            await _productRepository.Edit(product);

                            await _productMapOrderDetailRepository.Add(productMapOrder);

                            //productMapOrder.Product = new Product
                            //{
                            //    Price = Math.Round(product.Price * exchangeRate, 2),
                            //    PromotionRate = product.PromotionRate,
                            //    Id = product.Id,
                            //    Name = product.Name
                            //};
                        }
                        newProductOrder.ProductTotalAmount = totalAmount;
                        newProductOrder.TotalCost = totalCostInVND;

                        await _productOrderRepository.Edit(newProductOrder);

                        newProductOrder.TotalCost = totalCostInUSD;
                    }

                    var createdPayment = _paypalService.CreatePayment(newProductOrder, "sale", exchangeRate);

                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment
                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    productOrderPaypalResult.OrderId = newProductOrder.OrderId;
                    productOrderPaypalResult.paypal_redirect = paypalRedirectUrl;

                    EmailSender sender = new EmailSender
                    {
                        ReceiverEmail = newProductOrder.Customer.Email
                    };
                    var emailResponse = await _emailService.SendOrderSuccessEmail(newProductOrder, sender, EmailOption.SendGridApiKey, EmailOption.OrderSuccessTemplateId);

                    result.Content = productOrderPaypalResult;
                    result.Succeed = true;
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }

            return result;
        }

        public async Task<Result<string>> AddProductOrderPaypal(string paymentId, string payerId)
        {
            var executedPayment = _paypalService.ExecutePayment(paymentId, payerId);
            var result = new Result<string>();

            try
            {
                var orderId = executedPayment.transactions[0].invoice_number;
                var productOrder = await _productOrderRepository.Get(orderId);
                productOrder.OrderStatus = OrderStatus.Pending;

                //ProductOrder newProductOrder = new ProductOrder();
                //Customer cust = new Customer
                //{
                //    FullName = $"{executedPayment.payer.payer_info.first_name} {executedPayment.payer.payer_info.last_name}",
                //    Email = executedPayment.payer.payer_info.email,
                //    Address = $"{executedPayment.payer.payer_info.shipping_address.line1}",
                //    Phone = executedPayment.payer.payer_info.phone
                //};
                //var customerId = await _customerRepository.Add(cust);

                //newProductOrder.CustomerId = customerId;
                //newProductOrder.Customer = null;
                //newProductOrder.OrderStatus = OrderStatus.Pending;
                //newProductOrder.OrderDate = DateTime.UtcNow;
                //newProductOrder.OrderId = executedPayment.transactions[0].invoice_number;
                //newProductOrder.PaymentMethod = PaymentMethod.Paypal;

                //var orderId = _productOrderRepository.Add(newProductOrder);

                //double totalCost = 0;
                ////long totalCost = (long)(long.Parse(executedPayment.transactions[0].amount.total) / exchangeRate);
                //long totalAmount = 0;
                //var products = executedPayment.transactions[0].item_list.items;
                //newProductOrder.ProductMapOrderDetails = new List<ProductMapOrderDetail>();
                //foreach (var item in products)
                //{
                //    var product = await _productRepository.GetBy(item.sku); // item.sku => product.id
                //    var quantity = long.Parse(item.quantity);
                //    totalCost += Math.Round((product.Price - (product.Price * product.PromotionRate) / 100) * quantity, 2);
                //    totalAmount += quantity;
                //    product.PurchaseCounter += quantity;

                //    var productMapOrder = new ProductMapOrderDetail
                //    {
                //        ProductId = product.Id,
                //        ProductOrderId = orderId,
                //        ProductAmount = quantity
                //    };
                //    newProductOrder.ProductMapOrderDetails.Add(productMapOrder);
                //    await _productRepository.Edit(product);

                //    await _productMapOrderDetailRepository.Add(productMapOrder);
                //}
                //newProductOrder.ProductTotalAmount = totalAmount;
                //newProductOrder.TotalCost = totalCost;

                //EmailSender sender = new EmailSender
                //{
                //    ReceiverEmail = newProductOrder.Customer.Email
                //};
                //var emailResponse = await _emailService.SendOrderSuccessEmail(newProductOrder, sender, EmailOption.SendGridApiKey, EmailOption.OrderSuccessTemplateId);

                await _productOrderRepository.Edit(productOrder);

                result.Content = orderId;
                result.Succeed = true;
            }
            catch (Exception ex)
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
                if (newProductOrder.OrderStatus == OrderStatus.Delivering)
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
                if (productOrder != null)
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
