using ShopWebsite.Common.Models.Enums;

namespace ShopWebsiteServerSide.Models.AccountModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string AvatarUrl { get; set; }
        public UserRole Role { get; set; }
    }
}
