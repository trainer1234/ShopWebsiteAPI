using ShopWebsite.DAL.Models.AccountModels;
using ShopWebsite.DAL.Models.ManufactureModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Models.CustomerModels
{
    public class UserHobby
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public string ManufactureId { get; set; }
        public bool IsDisabled { get; set; } = false;

        public User User { get; set; }
        public Manufacture Manufacture { get; set; }
    }
}
