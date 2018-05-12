using ShopWebsite.DAL.Models.AccountModels;
using ShopWebsite.DAL.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Models.CustomerModels
{
    public class CustomerProductRating
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public double Rating { get; set; }
        public double PredictedRating { get; set; }
        public int View { get; set; }
        public bool IsDisabled { get; set; } = false;

        public User User { get; set; }
        public Product Product { get; set; }
    }
}
