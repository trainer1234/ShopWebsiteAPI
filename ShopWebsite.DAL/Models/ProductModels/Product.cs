using ShopWebsite.Common.Models.Enums;
using ShopWebsite.DAL.Models.ManufactureModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Models.ProductModels
{
    public class Product
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ManufactureId { get; set; }
        public string Name { get; set; }
        public int ManufactureYear { get; set; }
        public double Price { get; set; }
        public ProductType Type { get; set; }
        public ProductSpecificType ProductSpecificType { get; set; }
        public bool PromotionAvailable { get; set; } = false;
        public double PromotionRate { get; set; }
        public long Remain { get; set; }
        public bool IsDisabled { get; set; } = false;
        public Manufacture Manufacture { get; set; }
        public List<ProductProperty> ProductProperties { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}
