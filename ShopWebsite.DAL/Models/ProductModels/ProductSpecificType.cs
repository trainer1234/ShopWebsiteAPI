using ShopWebsite.Common.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Models.ProductModels
{
    public class ProductSpecificType
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public ProductType Type { get; set; }
        public bool IsDisabled { get; set; } = false;
    }
}
