using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShopWebsite.BLL.Contracts;
using ShopWebsite.Common.Models.AccountModels;
using ShopWebsite.DAL.Context;
using ShopWebsite.DAL.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsiteServerSide.Controllers
{
    [Authorize]
    [Route("api/account")]
    public class AccountController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ShopWebsiteSqlContext _context;
        private IConfiguration _config;

        public AccountController(IConfiguration config, UserManager<User> userManager, SignInManager<User> signInManager, ShopWebsiteSqlContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _config = config;
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] SignInModel signInModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accountService = GetService<IAccountService>();
            var serviceResult = await accountService.SignIn(signInModel);
            if (serviceResult.Succeed)
            {
                var users = await accountService.GetUserAsync();
                var searchUser = users.Find(user => user.UserName == signInModel.UserName);
                return await CreateToken(searchUser);
            }
            return BadRequest(serviceResult);
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] SignUpModel signUpModel)
        {
            if (ModelState.IsValid)
            {
                var accountService = GetService<IAccountService>();
                var serviceResult = await accountService.AddAccount(signUpModel);
                if (serviceResult.Succeed)
                {
                    return Ok(serviceResult);
                }
                else
                {
                    return BadRequest(serviceResult);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [AllowAnonymous]
        [Route("logout")]
        [HttpPost]
        public async Task<IActionResult> LogoutAsync([FromHeader] string Authorization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var accessToken = SplitAuthorizationHeader(Authorization);
            var accountService = GetService<IAccountService>();
            var users = await accountService.GetUserAsync();
            var searchUser = users.Find(user => user.AuthToken == accessToken);

            var serviceResult = accountService.SignOut(searchUser);
            if (serviceResult.Succeed)
            {
                return Ok(serviceResult);
            }
            return BadRequest(serviceResult.Errors);
        }

        private async Task<IActionResult> CreateToken(User user)
        {
            if (user != null)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: _config["Tokens:Issuer"],
                    audience: _config["Tokens:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: creds
                );
                var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

                var accountService = GetService<IAccountService>();
                if (user != null && user.AuthToken != null)
                {
                    var handler = new JwtSecurityTokenHandler();
                    var oldJwt = handler.ReadToken(user.AuthToken);
                    if (oldJwt.ValidTo < DateTime.UtcNow)
                    {
                        user.AuthToken = accessToken;
                        await accountService.SetUserAsync(user);
                    }
                }
                else
                {
                    user.AuthToken = accessToken;
                    await accountService.SetUserAsync(user);
                }
                return Ok(new
                {
                    accessToken = user.AuthToken,
                    expiration = token.ValidTo,
                });
            }
            else return BadRequest(new
            {
                ErrorContent = "Account has been locked, contact administration for more detail"
            });
        }
    }
}
