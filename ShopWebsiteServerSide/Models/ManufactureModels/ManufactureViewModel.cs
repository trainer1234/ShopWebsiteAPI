using ShopWebsite.Common.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsiteServerSide.Models.ManufactureModels
{
    public class ManufactureViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<ProductType> Types { get; set; }
    }
}
