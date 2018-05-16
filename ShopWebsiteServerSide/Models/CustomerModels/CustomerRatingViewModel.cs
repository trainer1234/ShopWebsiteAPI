using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsiteServerSide.Models.CustomerModels
{
    public class CustomerRatingViewModel
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public double Rating { get; set; }
    }
}
