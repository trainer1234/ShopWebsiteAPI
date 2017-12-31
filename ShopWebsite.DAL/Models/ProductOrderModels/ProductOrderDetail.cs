using ShopWebsite.DAL.Models.CustomerModels;
using ShopWebsite.DAL.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Models.ProductOrderModels
{
    public class ProductOrderDetail
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public string ProductOrderId { get; set; }
        public string ProductMapOrderDetailId { get; set; }
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }
        public ProductOrder ProductOrder { get; set; }
        public List<ProductMapOrderDetail> ProductMapOrderDetails { get; set; }
        public bool IsDisabled { get; set; } = false;
    }
}
