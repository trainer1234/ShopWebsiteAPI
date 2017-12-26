using Microsoft.EntityFrameworkCore;
using ShopWebsite.DAL.Context;
using ShopWebsite.DAL.Contracts;
using ShopWebsite.DAL.Models.LogModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.DAL.Implementations
{
    public class ErrorLogRepository : IErrorLogRepository
    {
        private readonly ShopWebsiteSqlContext _context;

        public ErrorLogRepository(ShopWebsiteSqlContext context)
        {
            _context = context;
        }

        public void Add(Exception ex)
        {
            var errorLog = new ErrorLog
            {
                CreatedTime = DateTime.UtcNow
            };
            if (ex.InnerException == null)
                errorLog.Content = ex.ToString();
            else
                errorLog.Content = ex.InnerException.ToString();
            _context.Add(errorLog);
            _context.SaveChanges();
        }

        public async Task<List<ErrorLog>> GetList()
        {
            var errorLogs = await _context.ErrorLogs.OrderByDescending(error => error.CreatedTime).ToListAsync();
            return errorLogs;
        }
    }
}
