using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopWebsite.BLL.Contracts;
using ShopWebsite.Common.Models.BaseModels;
using ShopWebsiteServerSide.Models.CustomerModels;
using ShopWebsiteServerSide.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsiteServerSide.Controllers
{
    [Authorize]
    [Route("api/rating")]
    public class RatingController : BaseController
    {
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateRating([FromHeader] string Authorization, [FromBody] CustomerRatingViewModel customerRatingView)
        {
            var accountService = GetService<IAccountService>();
            var users = await accountService.GetUserAsync();
            var token = SplitAuthorizationHeader(Authorization);
            var searchUser = users.Find(user => user.AuthToken == token);

            var ratingService = GetService<IRatingService>();

            var parser = new ModelParser();
            customerRatingView.UserId = searchUser.Id;
            var customerRating = parser.ParseCustomerRatingFrom(customerRatingView);

            var serviceResult = ratingService.Update(customerRating);

            return Ok(serviceResult);
        }

        [HttpGet]
        [Route("get/{productId}")]
        public async Task<IActionResult> GetPastRating([FromHeader] string Authorization, string productId)
        {
            var accountService = GetService<IAccountService>();
            var users = await accountService.GetUserAsync();
            var token = SplitAuthorizationHeader(Authorization);
            var searchUser = users.Find(user => user.AuthToken == token);

            var ratingService = GetService<IRatingService>();

            var parser = new ModelParser();

            var serviceResult = ratingService.GetPastRating(searchUser.Id, productId);
            if (serviceResult.Succeed)
            {
                var result = new Result<CustomerRatingViewModel>();
                var customerRatingView = parser.ParseCustomerRatingViewFrom(serviceResult.Content);

                result.Content = customerRatingView;
                result.Succeed = true;

                return Ok(result);
            }
            else
            {
                return Ok(serviceResult);
            }
        }
    }
}
