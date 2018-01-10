using ShopWebsite.Common.Models.Enums;
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
        public RoleViewModel Role { get; set; }
    }
}
