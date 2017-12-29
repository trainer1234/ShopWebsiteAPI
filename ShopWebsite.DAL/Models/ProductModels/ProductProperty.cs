using ShopWebsite.DAL.Models.PropertyModels;
using System;

namespace ShopWebsite.DAL.Models.ProductModels
{
    public class ProductProperty
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ProductId { get; set; }
        public string PropertyId { get; set; }
        public string PropertyDetail { get; set; }
        public Product Product { get; set; }
        public Property Property { get; set; }
        public bool IsDisabled { get; set; } = false;
    }
}
