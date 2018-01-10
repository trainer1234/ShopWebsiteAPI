using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopWebsite.BLL.Contracts;
using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.Common.Models.Enums;
using ShopWebsiteServerSide.Models.SlideModels;
using ShopWebsiteServerSide.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsiteServerSide.Controllers
{
    [Authorize]
    [Route("api/slide")]
    public class SlideController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetSlidesAsync()
        {
            var slideServie = GetService<ISlideService>();
            var serviceResult = await slideServie.GetList();
            if (serviceResult.Succeed)
            {
                var result = new Result<List<SlideViewModel>>();
                var parser = new ModelParser();
                var slideViews = new List<SlideViewModel>();
                foreach (var slide in serviceResult.Content)
                {
                    var slideView = parser.ParseSlideViewFrom(slide);
                    slideViews.Add(slideView);
                }

                result.Content = slideViews;
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
        public async Task<IActionResult> GetSlideAsync(string id)
        {
            var slideServie = GetService<ISlideService>();
            var serviceResult = await slideServie.Get(id);
            if (serviceResult.Succeed)
            {
                var result = new Result<SlideViewModel>();
                var parser = new ModelParser();
                var slideView = parser.ParseSlideViewFrom(serviceResult.Content);

                result.Content = slideView;
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
        public async Task<IActionResult> ReceiveSlideAsync([FromHeader] string Authorization, [FromBody] SlideViewModel slideView)
        {
            var accessToken = SplitAuthorizationHeader(Authorization);
            var accountService = GetService<IAccountService>();
            var users = await accountService.GetUserAsync();
            var searchUser = users.Find(user => user.AuthToken == accessToken);

            if (searchUser.Role != UserRole.Admin && searchUser.Role != UserRole.Manager)
            {
                var result = new Result<bool>();

                result.Content = result.Succeed = false;
                result.Errors = new Dictionary<int, string>();
                result.Errors.Add(0, "You don't have permission to access this feature");

                return BadRequest(result);
            }

            var slideServie = GetService<ISlideService>();
            var parser = new ModelParser();
            var slide = parser.ParseSlideFrom(slideView);

            var serviceResult = await slideServie.AddSlide(slide, searchUser.Id);
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
        public async Task<IActionResult> EditSlideAsync([FromHeader] string Authorization, [FromBody] SlideViewModel slideView)
        {
            var accessToken = SplitAuthorizationHeader(Authorization);
            var accountService = GetService<IAccountService>();
            var users = await accountService.GetUserAsync();
            var searchUser = users.Find(user => user.AuthToken == accessToken);

            if (searchUser.Role != UserRole.Admin && searchUser.Role != UserRole.Manager)
            {
                var result = new Result<bool>();

                result.Content = result.Succeed = false;
                result.Errors = new Dictionary<int, string>();
                result.Errors.Add(0, "You don't have permission to access this feature");

                return BadRequest(result);
            }

            var slideServie = GetService<ISlideService>();
            var parser = new ModelParser();
            var slide = parser.ParseSlideFrom(slideView);
            slide.Id = slideView.Id;

            var serviceResult = await slideServie.Edit(slide, searchUser.Id);
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
        public async Task<IActionResult> RemoveSlideAsync([FromHeader] string Authorization, string slideId)
        {
            var accessToken = SplitAuthorizationHeader(Authorization);
            var accountService = GetService<IAccountService>();
            var users = await accountService.GetUserAsync();
            var searchUser = users.Find(user => user.AuthToken == accessToken);

            if (searchUser.Role != UserRole.Admin && searchUser.Role != UserRole.Manager)
            {
                var result = new Result<bool>();

                result.Content = result.Succeed = false;
                result.Errors = new Dictionary<int, string>();
                result.Errors.Add(0, "You don't have permission to access this feature");

                return BadRequest(result);
            }

            var slideServie = GetService<ISlideService>();
            var serviceResult = await slideServie.Remove(slideId, searchUser.Id);
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
