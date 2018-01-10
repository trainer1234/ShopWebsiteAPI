using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopWebsite.BLL.Contracts;
using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.Common.Models.Enums;
using ShopWebsiteServerSide.Models.ProductModels;
using ShopWebsiteServerSide.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsiteServerSide.Controllers
{
    [Authorize]
    [Route("api/product")]
    public class ProductController : BaseController
    {
        [AllowAnonymous]
        [Route("get")]
        public async Task<IActionResult> GetAllProduct()
        {
            var productService = GetService<IProductService>();
            var serviceResult = await productService.GetAllProduct();

            if (serviceResult.Succeed)
            {
                var parser = new ModelParser();
                var result = new Result<List<ProductViewModel>>();
                var productViews = new List<ProductViewModel>();
                foreach (var product in serviceResult.Content)
                {
                    var productView = parser.ParseProductViewFrom(product);
                    productViews.Add(productView);
                }
                result.Content = productViews;
                result.Succeed = true;

                return Ok(result);
            }
            else
            {
                return Ok(serviceResult);
            }
        }
        
        [AllowAnonymous]
        [Route("get/{id}")]
        public async Task<IActionResult> GetProductBy(string id)
        {
            var productService = GetService<IProductService>();
            var serviceResult = await productService.GetProductBy(id);

            if (serviceResult.Succeed)
            {
                var parser = new ModelParser();
                var result = new Result<ProductViewModel>();
                var productView = parser.ParseProductViewFrom(serviceResult.Content);

                result.Content = productView;
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
        [Route("filter/{type}")]
        public async Task<IActionResult> FilterProductBy(ProductType type)
        {
            var productService = GetService<IProductService>();
            var serviceResult = await productService.GetAllProductBy(type);

            if (serviceResult.Succeed)
            {
                var parser = new ModelParser();
                var result = new Result<List<ProductViewModel>>();
                var productViews = new List<ProductViewModel>();
                foreach (var product in serviceResult.Content)
                {
                    var productView = parser.ParseProductViewFrom(product);
                    productViews.Add(productView);
                }
                result.Content = productViews;
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
        [Route("get-recent")]
        public async Task<IActionResult> GetRecentProductBy(ProductType type, int num = 0)
        {
            var productService = GetService<IProductService>();
            var serviceResult = await productService.GetRecentProductBy(type, num);

            if (serviceResult.Succeed)
            {
                var parser = new ModelParser();
                var result = new Result<List<ProductViewModel>>();
                var productViews = new List<ProductViewModel>();
                foreach (var product in serviceResult.Content)
                {
                    var productView = parser.ParseProductViewFrom(product);
                    productViews.Add(productView);
                }
                result.Content = productViews;
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
        [Route("search/{key}")]
        public async Task<IActionResult> SearchProduct(string key)
        {
            var productService = GetService<IProductService>();
            var serviceResult = await productService.SearchProduct(key);

            if (serviceResult.Succeed)
            {
                var parser = new ModelParser();
                var result = new Result<List<ProductViewModel>>();
                var productViews = new List<ProductViewModel>();
                foreach (var product in serviceResult.Content)
                {
                    var productView = parser.ParseProductViewFrom(product);
                    productViews.Add(productView);
                }
                result.Content = productViews;
                result.Succeed = true;

                return Ok(result);
            }
            else
            {
                return Ok(serviceResult);
            }
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddProduct([FromHeader] string Authorization, [FromBody] ProductViewModel productView)
        {
            var accountService = GetService<IAccountService>();
            var users = await accountService.GetUserAsync();
            var token = SplitAuthorizationHeader(Authorization);
            var searchUser = users.Find(user => user.AuthToken == token);
            if(searchUser.Role != UserRole.Admin && searchUser.Role != UserRole.Manager)
            {
                var result = new Result<bool>();

                result.Succeed = result.Content = false;
                result.Errors = new Dictionary<int, string>();
                result.Errors.Add(0, "You don't have permission to access this feature");

                return BadRequest(result);
            }

            var parser = new ModelParser();
            var product = parser.ParseProductFrom(productView);

            var productService = GetService<IProductService>();
            var serviceResult = await productService.AddProduct(product);

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
        public async Task<IActionResult> EditProduct([FromHeader] string Authorization, [FromBody] ProductViewModel productView)
        {
            var accountService = GetService<IAccountService>();
            var users = await accountService.GetUserAsync();
            var token = SplitAuthorizationHeader(Authorization);
            var searchUser = users.Find(user => user.AuthToken == token);
            if (searchUser.Role != UserRole.Admin && searchUser.Role != UserRole.Manager)
            {
                var result = new Result<bool>();

                result.Succeed = result.Content = false;
                result.Errors = new Dictionary<int, string>();
                result.Errors.Add(0, "You don't have permission to access this feature");

                return BadRequest(result);
            }

            var parser = new ModelParser();
            var product = parser.ParseProductFrom(productView);
            product.Id = productView.Id;

            var productService = GetService<IProductService>();
            var serviceResult = await productService.EditProduct(product);

            if (serviceResult.Succeed)
            {
                return Ok(serviceResult);
            }
            else
            {
                return BadRequest(serviceResult);
            }
        }

        [HttpPost]
        [Route("remove")]
        public async Task<IActionResult> RemoveProduct([FromHeader] string Authorization, string productId)
        {
            var accountService = GetService<IAccountService>();
            var users = await accountService.GetUserAsync();
            var token = SplitAuthorizationHeader(Authorization);
            var searchUser = users.Find(user => user.AuthToken == token);
            if (searchUser.Role != UserRole.Admin && searchUser.Role != UserRole.Manager)
            {
                var result = new Result<bool>();

                result.Succeed = result.Content = false;
                result.Errors = new Dictionary<int, string>();
                result.Errors.Add(0, "You don't have permission to access this feature");

                return BadRequest(result);
            }

            var productService = GetService<IProductService>();
            var serviceResult = await productService.RemoveProduct(productId);

            if (serviceResult.Succeed)
            {
                return Ok(serviceResult);
            }
            else
            {
                return BadRequest(serviceResult);
            }
        }
        
        [HttpPost]
        [Route("increase")]
        public async Task<IActionResult> IncreaseRemain([FromHeader] string Authorization, string productId, long amount = 0)
        {
            var accountService = GetService<IAccountService>();
            var users = await accountService.GetUserAsync();
            var token = SplitAuthorizationHeader(Authorization);
            var searchUser = users.Find(user => user.AuthToken == token);
            if (searchUser.Role != UserRole.Admin && searchUser.Role != UserRole.Manager)
            {
                var result = new Result<bool>();

                result.Succeed = result.Content = false;
                result.Errors = new Dictionary<int, string>();
                result.Errors.Add(0, "You don't have permission to access this feature");

                return BadRequest(result);
            }

            var productService = GetService<IProductService>();
            var serviceResult = await productService.IncreaseRemain(productId, amount);

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
