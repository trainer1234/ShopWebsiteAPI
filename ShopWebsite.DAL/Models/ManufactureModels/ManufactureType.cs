using ShopWebsite.Common.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Models.ManufactureModels
{
    public class ManufactureType
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ManufactureId { get; set; }
        public ProductType Type { get; set; }
        public Manufacture Manufacture { get; set; }
        public bool IsDisabled { get; set; } = false;
    }
}
