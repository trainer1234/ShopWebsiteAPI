using ShopWebsite.Common.Models.Enums;
using ShopWebsite.Common.Utils;
using ShopWebsite.DAL.Models.CustomerModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Models.ProductOrderModels
{
    public class ProductOrder
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string OrderId { get; set; } = Generator.GenerateOrderId(6);
        public string CustomerId { get; set; }
        public long ProductTotalAmount { get; set; }
        public long TotalCost { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Customer Customer { get; set; }
        public List<ProductMapOrderDetail> ProductMapOrderDetails { get; set; }
        public bool IsDisabled { get; set; } = false;
    }
}
