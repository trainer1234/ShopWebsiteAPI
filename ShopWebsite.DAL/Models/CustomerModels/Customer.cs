using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Models.CustomerModels
{
    public class Customer
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool IsDisabled { get; set; } = false;
    }
}
