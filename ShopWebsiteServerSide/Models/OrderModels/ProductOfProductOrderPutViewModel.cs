using ShopWebsiteServerSide.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsiteServerSide.Models.OrderModels
{
    public class ProductOfProductOrderPutViewModel
    {
        public ProductViewModel Product { get; set; }
        public long Amount { get; set; }
    }
}
