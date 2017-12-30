using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopWebsite.BLL.Contracts;
using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.Common.Models.Enums;
using ShopWebsiteServerSide.Models.ManufactureModels;
using ShopWebsiteServerSide.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsiteServerSide.Controllers
{
    [Authorize]
    [Route("api/manufacture")]
    public class ManufactureController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAll()
        {
            var manufactureService = GetService<IManufactureService>();
            var serviceResult = await manufactureService.GetAllManufacture();
            if (serviceResult.Succeed)
            {
                var parser = new ModelParser();
                var result = new Result<List<ManufactureViewModel>>();
                var manufactureViews = new List<ManufactureViewModel>();
                foreach (var manufacture in serviceResult.Content)
                {
                    var manufactureView = parser.ParserManufactureViewFrom(manufacture);
                    manufactureViews.Add(manufactureView);
                }

                result.Content = manufactureViews;
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
        public async Task<IActionResult> GetBy(string id)
        {
            var manufactureService = GetService<IManufactureService>();
            var serviceResult = await manufactureService.GetManufactureBy(id);
            if (serviceResult.Succeed)
            {
                var parser = new ModelParser();
                var result = new Result<ManufactureViewModel>();
                var manufactureView = parser.ParserManufactureViewFrom(serviceResult.Content);

                result.Content = manufactureView;
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
        [Route("filter/{productType}")]
        public async Task<IActionResult> Filter(ProductType productType)
        {
            var manufactureService = GetService<IManufactureService>();
            var serviceResult = await manufactureService.FilterManufactureBy(productType);
            if (serviceResult.Succeed)
            {
                var parser = new ModelParser();
                var result = new Result<List<ManufactureViewModel>>();
                var manufactureViews = new List<ManufactureViewModel>();
                foreach (var manufacture in serviceResult.Content)
                {
                    var manufactureView = parser.ParserManufactureViewFrom(manufacture);
                    manufactureViews.Add(manufactureView);
                }

                result.Content = manufactureViews;
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
        public async Task<IActionResult> AddNewManufacture([FromBody] ManufactureViewModel manufactureView)
        {
            var parser = new ModelParser();
            var manufactureService = GetService<IManufactureService>();
            var manufacture = parser.ParseManufactureFrom(manufactureView);
            var serviceResult = await manufactureService.AddManufacture(manufacture);
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
        public async Task<IActionResult> EditManufacture([FromBody] ManufactureViewModel manufactureView)
        {
            var parser = new ModelParser();
            var manufactureService = GetService<IManufactureService>();
            var manufacture = parser.ParseManufactureFrom(manufactureView);
            var serviceResult = await manufactureService.EditManufacture(manufacture);
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
        public async Task<IActionResult> RemoveManufacture(string manufactureId)
        {
            var manufactureService = GetService<IManufactureService>();
            var serviceResult = await manufactureService.RemoveManufacture(manufactureId);
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
