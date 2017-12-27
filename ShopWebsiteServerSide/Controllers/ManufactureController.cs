using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopWebsite.BLL.Contracts;
using ShopWebsite.Common.Models.BaseModels;
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

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetBy(string id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddNewManufacture(ManufactureViewModel manufactureView)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> EditManufacture(ManufactureViewModel manufactureView)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("remove")]
        public async Task<IActionResult> RemoveManufacture(string manufactureId)
        {
            throw new NotImplementedException();
        }
    }
}
