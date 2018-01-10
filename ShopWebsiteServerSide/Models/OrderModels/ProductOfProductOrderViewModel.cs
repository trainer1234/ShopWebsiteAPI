using ShopWebsite.DAL.Models.ProductModels;
using ShopWebsiteServerSide.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsiteServerSide.Models.OrderModels
{
    public class ProductOfProductOrderViewModel
    {
        public long Amount { get; set; }
        public ProductViewModel Product { get; set; }
    }
}
