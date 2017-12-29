using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Models.PropertyModels
{
    public class Property
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public bool IsDisabled { get; set; } = false;
    }
}
