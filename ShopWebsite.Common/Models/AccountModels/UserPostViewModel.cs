using ShopWebsite.Common.Models.Enums;
using ShopWebsite.Common.Models.ManufactureModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.Common.Models.AccountModels
{
    public class UserPostViewModel
    {
        public string UserName { get; set; }
        public string AvatarUrl { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public IncomeLimit Income { get; set; }
        public UserRole Role { get; set; }

        public List<ManufactureViewModel> Hobbies { get; set; }
    }
}
