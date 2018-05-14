using ShopWebsite.Common.Models.Enums;
using ShopWebsite.Common.Models.ManufactureModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.Common.Models.AccountModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string AvatarUrl { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public IncomeLimit Income { get; set; }
        public RoleViewModel Role { get; set; }

        public List<ManufactureViewModel> Hobbies { get; set; }
    }
}
