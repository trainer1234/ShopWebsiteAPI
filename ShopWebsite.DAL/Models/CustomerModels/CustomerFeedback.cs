using ShopWebsite.DAL.Models.AccountModels;
using ShopWebsite.DAL.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Models.CustomerModels
{
    public class CustomerFeedback
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public string Comment { get; set; }
        public bool IsDisabled { get; set; } = false;

        public User User { get; set; }
        public Product Product { get; set; }
    }
}
