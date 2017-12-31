using ShopWebsite.Common.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Models.ProductOrderModels
{
    public class ProductOrder
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string OrderId { get; set; }
        public string ProductOrderDetailId { get; set; }
        public int ProductAmount { get; set; }
        public double TotalCost { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public ProductOrderDetail ProductOrderDetail { get; set; }
        public bool IsDisabled { get; set; } = false;
    }
}
