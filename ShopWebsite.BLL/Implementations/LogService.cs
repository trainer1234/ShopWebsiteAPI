using ShopWebsite.BLL.Contracts;
using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.DAL.Contracts;
using ShopWebsite.DAL.Models.LogModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.BLL.Implementations
{
    public class LogService : ILogService
    {
        private IErrorLogRepository _errorLogRepository;

        public LogService(IErrorLogRepository errorLogRepository)
        {
            _errorLogRepository = errorLogRepository;
        }

        public async Task<Result<List<ErrorLog>>> GetErrorLogs()
        {
            var result = new Result<List<ErrorLog>>();
            var errorLogs = await _errorLogRepository.GetList();
            if (errorLogs != null && errorLogs.Count > 0)
            {
                result.Content = errorLogs;
                result.Succeed = true;
            }
            else
            {
                result.Succeed = false;
                result.Errors = new Dictionary<int, string>();
                result.Errors.Add(5, "No logs available");
            }
            return result;
        }
    }
}
