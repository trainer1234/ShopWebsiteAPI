using ShopWebsite.Common.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Models.ProductModels
{
    public class Product
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ProductSpecificTypeId { get; set; }
        public string Name { get; set; }
        public string ManufactureYear { get; set; }
        public string ManufactureName { get; set; }
        public string ProductImageUrl { get; set; }
        public double Price { get; set; }
        public ProductType Type { get; set; }
        public bool IsDisabled { get; set; } = false;
        public ProductSpecificType ProductSpecificType { get; set; }
    }
}
