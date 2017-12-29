using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopWebsite.BLL.Contracts;
using ShopWebsite.Common.Models.BaseModels;
using ShopWebsiteServerSide.Models.PropertyModels;
using ShopWebsiteServerSide.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopWebsiteServerSide.Controllers
{
    [Authorize]
    [Route("api/property")]
    public class PropertyController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAll()
        {
            var propertyService = GetService<IPropertyService>();
            var serviceResult = await propertyService.GetAllProperty();
            if (serviceResult.Succeed)
            {
                var parser = new ModelParser();
                var result = new Result<List<PropertyViewModel>>();
                var propertyViews = new List<PropertyViewModel>();
                foreach (var property in serviceResult.Content)
                {
                    var propertyView = parser.ParsePropertyViewFrom(property);
                    propertyViews.Add(propertyView);
                }

                result.Content = propertyViews;
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
            var propertyService = GetService<IPropertyService>();
            var serviceResult = await propertyService.GetPropertyBy(id);
            if (serviceResult.Succeed)
            {
                var parser = new ModelParser();
                var result = new Result<PropertyViewModel>();
                var propertyView = parser.ParsePropertyViewFrom(serviceResult.Content);

                result.Content = propertyView;
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
        public async Task<IActionResult> AddNewProperty([FromBody] PropertyViewModel propertyView)
        {
            var parser = new ModelParser();
            var propertyService = GetService<IPropertyService>();
            var property = parser.ParsePropertyFrom(propertyView);
            var serviceResult = await propertyService.AddProperty(property);
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
        public async Task<IActionResult> EditProperty([FromBody] PropertyViewModel propertyView)
        {
            var parser = new ModelParser();
            var propertyService = GetService<IPropertyService>();
            var property = parser.ParsePropertyFrom(propertyView);
            var serviceResult = await propertyService.EditProperty(property);
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
        public async Task<IActionResult> RemoveProperty(string propertyId)
        {
            var propertyService = GetService<IPropertyService>();
            var serviceResult = await propertyService.RemoveProperty(propertyId);
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
