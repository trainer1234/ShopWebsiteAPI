using ShopWebsite.DAL.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Models.ProductOrderModels
{
    public class ProductMapOrderDetail
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ProductId { get; set; }
        public string ProductOrderDetailId { get; set; }
        public Product Product { get; set; }
        public ProductOrder ProductOrder { get; set; }
        public bool IsDisabled { get; set; } = false;
    }
}
