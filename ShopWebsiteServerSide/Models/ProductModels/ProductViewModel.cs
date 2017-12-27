using ShopWebsite.Common.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsiteServerSide.Models.ProductModels
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ManufactureYear { get; set; }
        public string ManufactureName { get; set; }
        public string ProductImageUrl { get; set; }
        public double Price { get; set; }
        public ProductType Type { get; set; }
        public ProductSpecificType SpecificType { get; set; }
    }
}
