﻿using ShopWebsite.Common.Models.Enums;
using ShopWebsiteServerSide.Models.ManufactureModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsiteServerSide.Models.ProductModels
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ManufactureYear { get; set; }
        public double Price { get; set; }
        public bool PromotionAvailable { get; set; } = false;
        public double PromotionRate { get; set; }
        public ProductType Type { get; set; }
        public ProductSpecificType SpecificType { get; set; }
        [Required]
        public ManufactureViewModel Manufacture { get; set; }
        public List<string> ProductImageUrls { get; set; }
    }
}