using ShopWebsite.Common.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsiteServerSide.Models.ProductModels
{
    public class ProductSpecificTypeViewModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public ProductType Type { get; set; }
    }
}
