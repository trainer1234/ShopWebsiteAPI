using ShopWebsite.DAL.Models.LogModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.DAL.Contracts
{
    public interface IErrorLogRepository
    {
        void Add(Exception ex);
        Task<List<ErrorLog>> GetList();
    }
}
