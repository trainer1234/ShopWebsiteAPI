using Microsoft.AspNetCore.Identity;
using ShopWebsite.Common.Models.Enums;
using System;

namespace ShopWebsite.DAL.Models.AccountModels
{
    public class User : IdentityUser
    {
        public string AuthToken { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public string AvatarUrl { get; set; }
        public UserRole Role { get; set; }
        public bool IsDisabled { get; set; } = false;
    }
}
