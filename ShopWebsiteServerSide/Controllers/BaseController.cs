using Microsoft.AspNetCore.Mvc;
using ShopWebsite.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsiteServerSide.Controllers
{
    public class BaseController : Controller
    {
        private readonly ShopWebsiteSqlContext _context;

        public BaseController()
        {

        }
        public BaseController(ShopWebsiteSqlContext context)
        {
            _context = context;
        }

        protected T GetService<T>()
        {
            try
            {
                var result = HttpContext.RequestServices.GetService(typeof(T));
                if (result != null && result is T)
                {
                    return (T)result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return default(T);
        }

        public string SplitAuthorizationHeader(string Authorization)
        {
            if (Authorization == null) return String.Empty;
            char[] delimiter = { ' ' };
            var splitAuthorizationString = Authorization.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
            return splitAuthorizationString[1];
        }
    }
}
