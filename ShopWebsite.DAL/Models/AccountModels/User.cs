using Microsoft.AspNetCore.Identity;
using ShopWebsite.Common.Models.Enums;
using ShopWebsite.DAL.Models.CustomerModels;
using ShopWebsite.DAL.Models.ManufactureModels;
using System;
using System.Collections.Generic;

namespace ShopWebsite.DAL.Models.AccountModels
{
    public class User : IdentityUser
    {
        public string AuthToken { get; set; }
        public string AvatarUrl { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public IncomeLimit Income { get; set; }
        public UserRole Role { get; set; }
        public bool IsDisabled { get; set; } = false;

        public List<UserHobby> UserHobbies { get; set; }
    }
}
