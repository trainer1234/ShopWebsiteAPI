using ShopWebsite.DAL.Models.AccountModels;
using ShopWebsite.DAL.Models.CustomerModels;
using ShopWebsite.DAL.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Contracts
{
    public interface IRatingRepository
    {
        void Update(CustomerRating customerRating);
        CustomerRating GetPastRating(string userId, string productId);
        List<Product> GetNRecommendedProduct(User user, int n);
    }
}
