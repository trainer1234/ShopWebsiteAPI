using ShopWebsite.Common.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopWebsite.Common.Models.AccountModels
{
    public class SignUpModel
    {
        [Required]
        public string Username { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        public string AvatartUrl { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
    }
}
