using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopWebsite.BLL.Contracts;
using ShopWebsite.Common.Models.BaseModels;
using ShopWebsiteServerSide.Models.ProductModels;
using ShopWebsiteServerSide.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsiteServerSide.Controllers
{
    [Authorize]
    [Route("api/product-specific-type")]
    public class ProductSpecificTypeController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAllType()
        {
            var productSpecificTypeService = GetService<IProductSpecificTypeService>();
            var serviceResult = await productSpecificTypeService.GetAllProductSpecificType();
            if (serviceResult.Succeed)
            {
                var parser = new ModelParser();
                var result = new Result<List<ProductSpecificTypeViewModel>>();
                var productSpecificTypeViews = new List<ProductSpecificTypeViewModel>();
                foreach (var type in serviceResult.Content)
                {
                    var typeView = parser.ParseProductSpecificTypeViewFrom(type);
                    productSpecificTypeViews.Add(typeView);
                }

                result.Content = productSpecificTypeViews;
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
        public async Task<IActionResult> GetTypeBy(string id)
        {
            var productSpecificTypeService = GetService<IProductSpecificTypeService>();
            var serviceResult = await productSpecificTypeService.GetProductSpecificTypeBy(id);
            if (serviceResult.Succeed)
            {
                var parser = new ModelParser();
                var result = new Result<ProductSpecificTypeViewModel>();
                var typeView = parser.ParseProductSpecificTypeViewFrom(serviceResult.Content);

                result.Content = typeView;
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
        public async Task<IActionResult> AddNewType([FromBody] ProductSpecificTypeViewModel productSpecificTypeView)
        {
            var parser = new ModelParser();
            var productSpecificType = parser.ParseProductSpecificTypeFrom(productSpecificTypeView);
            var productSpecificTypeService = GetService<IProductSpecificTypeService>();
            var serviceResult = await productSpecificTypeService.AddProductSpecificType(productSpecificType);
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
        public async Task<IActionResult> EditNewType([FromBody] ProductSpecificTypeViewModel productSpecificTypeView)
        {
            var parser = new ModelParser();
            var productSpecificType = parser.ParseProductSpecificTypeFrom(productSpecificTypeView);
            var productSpecificTypeService = GetService<IProductSpecificTypeService>();
            var serviceResult = await productSpecificTypeService.EditProductSpeicificType(productSpecificType);
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
        public async Task<IActionResult> RemoveNewType(string productSpecificTypeId)
        {
            var productSpecificTypeService = GetService<IProductSpecificTypeService>();
            var serviceResult = await productSpecificTypeService.RemoveProductSpecificType(productSpecificTypeId);
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
