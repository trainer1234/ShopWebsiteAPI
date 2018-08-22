using ShopWebsite.Common.Models.Enums;
using ShopWebsite.Common.Utils;
using ShopWebsite.DAL.Models.ProductModels;
using ShopWebsiteServerSide.Models.CustomerModels;
using ShopWebsiteServerSide.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsiteServerSide.Models.OrderModels
{
    public class ProductOrderViewModel
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public long ProductAmount { get; set; }
        public long TotalCost { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public CustomerViewModel Customer { get; set; }
        public List<ProductOfProductOrderViewModel> Products { get; set; }
    }
}
