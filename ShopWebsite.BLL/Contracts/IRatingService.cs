using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.DAL.Models.CustomerModels;
using ShopWebsite.DAL.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.BLL.Contracts
{
    public interface IRatingService
    {
        Result<bool> Update(CustomerRating newCustomerRating);
        Result<CustomerRating> GetPastRating(string userId, string productId);
        Result<List<Product>> GetTopNRecommendedProduct(string userId, int n);
    }
}
