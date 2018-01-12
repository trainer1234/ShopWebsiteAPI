using ShopWebsite.Common.Models.Enums;
using ShopWebsiteServerSide.Models.CustomerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsiteServerSide.Models.OrderModels
{
    public class ProductOrderPutViewModel
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public int ProductAmount { get; set; }
        public double TotalCost { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public CustomerViewModel Customer { get; set; }
        public List<ProductOfProductOrderPutViewModel> Products { get; set; }
    }
}
