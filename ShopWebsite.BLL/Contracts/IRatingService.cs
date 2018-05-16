using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.DAL.Models.CustomerModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.BLL.Contracts
{
    public interface IRatingService
    {
        Result<bool> Add(CustomerRating customerRating);
        Result<bool> Update(CustomerRating newCustomerRating);
        Result<CustomerRating> GetPastRating(string userId, string productId);
    }
}
