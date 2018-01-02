using Microsoft.AspNetCore.Authorization;
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

            var serviceResult = await productOrderService.AddProductOrder(productOrder);
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
        public async Task<IActionResult> EditOrder([FromBody] ProductOrderPostViewModel productOrderView)
        {
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
                return BadRequest(serviceResult);
            }
        }

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
                return BadRequest(serviceResult);
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
