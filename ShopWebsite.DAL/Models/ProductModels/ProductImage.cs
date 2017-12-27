using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Models.ProductModels
{
    public class ProductImage
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ImageUrl { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public bool IsDisabled { get; set; } = false;
    }
}
