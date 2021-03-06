﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopWebsite.BLL.Contracts;
using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.Common.Models.Enums;
using ShopWebsiteServerSide.Models.OrderModels;
using ShopWebsiteServerSide.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsiteServerSide.Controllers
{
    [Authorize]
    [Route("api/order")]
    public class ProductOrderController : BaseController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddOrder([FromBody] ProductOrderPostViewModel productOrderView)
        {
            if(productOrderView.OrderStatus != OrderStatus.NotConfirmed || productOrderView.OrderId != null)
            {
                var result = new Result<bool>
                {
                    Content = false,
                    Succeed = false,
                    Errors = new Dictionary<int, string>()
                };
                result.Errors.Add(23, "Order Status Must be in Not Confirmed state and order id must be null");

                return BadRequest(result);
            }
            var productOrderService = GetService<IProductOrderService>();
            var parser = new ModelParser();
            var productOrder = parser.ParseProductOrderFrom(productOrderView);

            var serviceResult = await productOrderService.AddProductOrder(productOrder, productOrderView.PaymentMethod);
            if (serviceResult.Succeed)
            {
                return Ok(serviceResult);
            }
            else
            {
                return BadRequest(serviceResult);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("execute-paypal")]
        public async Task<IActionResult> ExecutePaymentFromPaypal(string paymentId, string payerId)
        {
            var productOrderService = GetService<IProductOrderService>();
            var serviceResult = await productOrderService.AddProductOrderPaypal(paymentId, payerId);

            if (serviceResult.Succeed)
            {
                return Ok(serviceResult);
            }
            else
            {
                return BadRequest(serviceResult);
            }
        }

        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> EditOrder([FromHeader] string Authorization, [FromBody] ProductOrderPostViewModel productOrderView)
        {
            var accountService = GetService<IAccountService>();
            var users = await accountService.GetUserAsync();
            var token = SplitAuthorizationHeader(Authorization);
            var searchUser = users.Find(user => user.AuthToken == token);
            if (searchUser.Role != UserRole.Admin && searchUser.Role != UserRole.Manager && searchUser.Role != UserRole.Staff)
            {
                var result = new Result<bool>();

                result.Succeed = result.Content = false;
                result.Errors = new Dictionary<int, string>();
                result.Errors.Add(0, "You don't have permission to access this feature");

                return BadRequest(result);
            }

            var productOrderService = GetService<IProductOrderService>();
            var parser = new ModelParser();
            var productOrder = parser.ParseProductOrderFrom(productOrderView);

            var serviceResult = await productOrderService.EditProductOrder(productOrder);
            if (serviceResult.Succeed)
            {
                return Ok(serviceResult);
            }
            else
            {
                return BadRequest(serviceResult);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetOrder()
        {
            var productOrderService = GetService<IProductOrderService>();

            var serviceResult = await productOrderService.GetAll();
            if (serviceResult.Succeed)
            {
                var result = new Result<List<ProductOrderViewModel>>();

                var parser = new ModelParser();
                var productOrderViews = new List<ProductOrderViewModel>();
                foreach (var order in serviceResult.Content)
                {
                    var productOrderView = parser.ParseProductOrderViewFrom(order);
                    productOrderViews.Add(productOrderView);
                }
                result.Content = productOrderViews;
                result.Succeed = true;

                return Ok(result);
            }
            else
            {
                return Ok(serviceResult);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetOrderBy(string id)
        {
            var productOrderService = GetService<IProductOrderService>();

            var serviceResult = await productOrderService.Get(id);
            if (serviceResult.Succeed)
            {
                var result = new Result<ProductOrderViewModel>();

                var parser = new ModelParser();
                var productOrderView = parser.ParseProductOrderViewFrom(serviceResult.Content);

                result.Content = productOrderView;
                result.Succeed = true;

                return Ok(result);
            }
            else
            {
                return Ok(serviceResult);
            }
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("filter/{orderStatus}")]
        public async Task<IActionResult> FilterOrderBy(OrderStatus orderStatus)
        {
            var productOrderService = GetService<IProductOrderService>();

            var serviceResult = await productOrderService.FilterBy(orderStatus);
            if (serviceResult.Succeed)
            {
                var result = new Result<List<ProductOrderViewModel>>();

                var parser = new ModelParser();
                var productOrderViews = new List<ProductOrderViewModel>();
                foreach (var order in serviceResult.Content)
                {
                    var productOrderView = parser.ParseProductOrderViewFrom(order);
                    productOrderViews.Add(productOrderView);
                }
                result.Content = productOrderViews;
                result.Succeed = true;

                return Ok(result);
            }
            else
            {
                return Ok(serviceResult);
            }
        }

        [HttpPost]
        [Route("remove")]
        public async Task<IActionResult> RemoveOrder(string id)
        {
            var productOrderService = GetService<IProductOrderService>();
            var serviceResult = await productOrderService.RemoveProductOrder(id);
            if (serviceResult.Succeed)
            {
                return Ok(serviceResult);
            }
            else
            {
                return BadRequest(serviceResult);
            }
        }
    }
}
