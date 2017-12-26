using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebsite.DAL.Models.AccountModels
{
    public class User : IdentityUser
    {
        public string AuthToken { get; set; }
        public string AvatarUrl { get; set; }
        public bool IsDisabled { get; set; } = false;
    }
}
