using ShopWebsite.BLL.Implementations;
using ShopWebsite.Common.Models.AccountModels;
using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.DAL.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.BLL.Contracts
{
    public interface IAccountService
    {
        Task<List<User>> GetUserAsync();
        Task<bool> SetUserAsync(User user);
        Task<Result<bool>> EditUser(UserViewModel newUserView);
        Task<Result<bool>> RemoveUser(string userName);
        Task<Result<bool>> SignIn(SignInModel signInModel);
        Result<bool> SignOut(User user);
        Task<Result<bool>> AddAccount(SignUpModel signUpModel);
    }
}
