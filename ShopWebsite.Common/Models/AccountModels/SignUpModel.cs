using ShopWebsite.Common.Models.Enums;
using ShopWebsite.Common.Models.ManufactureModels;
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
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string AvatartUrl { get; set; }
        public IncomeLimit Income { get; set; }
        public UserRole Role { get; set; }

        public List<ManufactureViewModel> Hobbies { get; set; }
    }
}
