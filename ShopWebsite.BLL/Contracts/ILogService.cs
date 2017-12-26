using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.DAL.Models.LogModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.BLL.Contracts
{
    public interface ILogService
    {
        Task<Result<List<ErrorLog>>> GetErrorLogs();
    }
}
