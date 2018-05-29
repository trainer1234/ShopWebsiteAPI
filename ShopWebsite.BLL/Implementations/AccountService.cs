using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopWebsite.BLL.Contracts;
using ShopWebsite.Common.Models.AccountModels;
using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.DAL.Context;
using ShopWebsite.DAL.Contracts;
using ShopWebsite.DAL.Models.AccountModels;
using ShopWebsite.DAL.Models.CustomerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.BLL.Implementations
{
    public class AccountService : IAccountService
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IErrorLogRepository _errorLogRepository;
        private IRecommenderService _recommenderService;
        //private IActivityLogRepository _activityLogRepository;
        //private IUserImageRepository _userImageRepository;
        private readonly ShopWebsiteSqlContext _context;

        public AccountService()
        {
        }

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, ShopWebsiteSqlContext context,
            IErrorLogRepository errorLogRepository, IRecommenderService recommenderService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _errorLogRepository = errorLogRepository;
            _recommenderService = recommenderService;
        }

        public async Task<List<User>> GetUserAsync()
        {
            var result = await _userManager.Users.Include(user => user.UserHobbies)
                                                    .ThenInclude(hobby => hobby.Manufacture)
                                                    .ThenInclude(m => m.ManufactureTypes).ToListAsync();
            return result;
        }

        public async Task<bool> SetUserAsync(User user)
        {
            var userExist = await _userManager.FindByNameAsync(user.UserName);
            var result = new IdentityResult();
            if (userExist != null) result = await _userManager.UpdateAsync(user);
            else result = await _userManager.CreateAsync(user);
            return result.Succeeded;
        }

        public async Task<Result<bool>> EditUser(UserPostViewModel newUserView)
        {
            var userExist = await _userManager.FindByNameAsync(newUserView.UserName);
            var result = new Result<bool>();
            try
            {
                if (userExist != null)
                {
                    userExist.Role = newUserView.Role;
                    userExist.AvatarUrl = newUserView.AvatarUrl;
                    userExist.Income = newUserView.Income;
                    userExist.FullName = newUserView.FullName;
                    userExist.Birthday = newUserView.Birthday;

                    await _userManager.UpdateAsync(userExist);

                    if(newUserView.Password != null)
                    {
                        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(userExist);
                        await _userManager.ResetPasswordAsync(userExist, resetToken, newUserView.Password);
                    }

                    if (newUserView.Hobbies != null && newUserView.Hobbies.Count > 0)
                    {
                        var oldHobbiesOfUser = _context.UserHobbies.Where(userHobby => userHobby.UserId == userExist.Id);
                        foreach (var userHobby in oldHobbiesOfUser)
                        {
                            _context.Remove(userHobby);
                        }
                        foreach (var hobby in newUserView.Hobbies)
                        {
                            var userHobby = new UserHobby
                            {
                                ManufactureId = hobby.Id,
                                UserId = userExist.Id
                            };
                            _context.Add(userHobby);
                        }

                        _context.SaveChanges();
                    }

                    result.Succeed = result.Content = true;
                }
                else
                {
                    result.Content = result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(0, "No user");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
            return result;
        }

        public async Task<Result<bool>> RemoveUser(string userName)
        {
            var userExist = await _userManager.FindByNameAsync(userName);
            var result = new Result<bool>();
            try
            {
                if (userExist != null)
                {
                    await _userManager.DeleteAsync(userExist);

                    result.Succeed = result.Content = true;
                }
                else
                {
                    result.Content = result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(0, "No user");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
            return result;
        }

        public async Task<Result<bool>> SignIn(SignInModel signInModel)
        {
            var result = new Result<bool>();
            var searchUser = new User();
            try
            {
                searchUser = await _userManager.FindByNameAsync(signInModel.UserName);
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }

            if (searchUser == null)
            {
                result.Succeed = false;
                result.Errors = new Dictionary<int, string>();
                result.Errors.Add(1, "User name or password is not correct");
            }
            else
            {
                var res = new SignInResult();
                try
                {
                    res = await _signInManager.PasswordSignInAsync(searchUser, signInModel.Password,
                        false, lockoutOnFailure: false);
                }
                catch (Exception ex)
                {
                    _errorLogRepository.Add(ex);
                    throw;
                }
                if (res.Succeeded)
                {
                    //var activityLog = new AccountActivityLog
                    //{
                    //    Activity = "Log in",
                    //    CreatedDate = DateTime.UtcNow,
                    //    UserId = searchUser.Id
                    //};
                    //_activityLogRepository.Add(activityLog);

                    result.Succeed = result.Content = true;
                }
                else
                {
                    result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(1, "User name or password is not correct");
                }
            }
            return result;
        }

        public Result<bool> SignOut(User user)
        {
            var result = new Result<bool>();
            if (user == null)
            {
                result.Content = false;
                result.Succeed = false;
                result.Errors = new Dictionary<int, string>();
                result.Errors.Add(4, "Cannot find user");
            }
            else
            {
                try
                {
                    //var activityLog = new AccountActivityLog
                    //{
                    //    Activity = "Log out",
                    //    CreatedDate = DateTime.UtcNow,
                    //    UserId = user.Id
                    //};
                    //_activityLogRepository.Add(activityLog);
                    result.Content = result.Succeed = true;
                }
                catch (Exception ex)
                {
                    _errorLogRepository.Add(ex);
                    throw;
                }
            }
            return result;
        }

        public async Task<Result<bool>> AddAccount(SignUpModel signUpModel)
        {
            var users = _context.Users.OrderBy(user => user.Index).ToList();
            var searchUser = users.Find(user => user.UserName == signUpModel.Username);
            var result = new Result<bool>();
            if (searchUser == null)
            {
                var user = new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    Index = users[users.Count - 1].Index + 1,
                    IsDisabled = false,
                    AvatarUrl = signUpModel.AvatartUrl,
                    UserName = signUpModel.Username,
                    Birthday = signUpModel.Birthday,
                    FullName = signUpModel.FullName,
                    Income = signUpModel.Income,
                    Role = signUpModel.Role
                };
                var userId = user.Id;
                var res = new IdentityResult();

                try
                {
                    res = await _userManager.CreateAsync(user, signUpModel.Password);
                    if (res.Succeeded)
                    {
                        //if (user.AvatarUrl != null)
                        //{
                        //    var handler = new ImageHandler();
                        //    var imageModelId = handler.GetImageId(ImageUrlOption.Original, user.AvatarUrl);
                        //    var userImage = new UserImage
                        //    {
                        //        ImageModelId = imageModelId,
                        //        UserId = user.Id
                        //    };
                        //    var addResult = await _userImageRepository.Add(userImage);
                        //    if (!addResult)
                        //    {
                        //        result.Errors = new Dictionary<int, string>();
                        //        result.Errors.Add(17, "Cannot add user image but user was successfully created");
                        //    }
                        //}

                        if (signUpModel.Hobbies != null && signUpModel.Hobbies.Count > 0)
                        {
                            foreach (var hobby in signUpModel.Hobbies)
                            {
                                var userHobby = new UserHobby
                                {
                                    ManufactureId = hobby.Id,
                                    UserId = userId
                                };
                                _context.Add(userHobby);
                            }

                            _context.SaveChanges();
                        }

                        _recommenderService.UpdateUserLatentFactorMatrixWhenAddingNewUser(userId);
                        _recommenderService.RecommendNewUser(userId);

                        result.Succeed = true;
                        result.Content = true;
                    }
                    else
                    {
                        result.Succeed = false;
                        result.Errors = new Dictionary<int, string>();
                        result.Errors.Add(2, "Cannot create user");
                        await _userManager.DeleteAsync(user);
                    }
                }
                catch (Exception ex)
                {
                    _errorLogRepository.Add(ex);
                    await _userManager.DeleteAsync(user);
                    throw;
                }
            }
            else
            {
                result.Succeed = false;
                result.Errors = new Dictionary<int, string>();
                result.Errors.Add(3, "Account already exist");
            }
            return result;
        }
    }
}
