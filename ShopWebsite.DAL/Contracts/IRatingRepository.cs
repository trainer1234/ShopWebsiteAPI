using ShopWebsite.DAL.Models.CustomerModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Contracts
{
    public interface IRatingRepository
    {
        void Update(CustomerRating customerRating);
        CustomerRating GetPastRating(string userId, string productId);
    }
}
