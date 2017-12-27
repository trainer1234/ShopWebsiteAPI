using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.DAL.Models.ManufactureModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.BLL.Contracts
{
    public interface IManufactureService
    {
        Task<Result<bool>> AddManufacture(Manufacture newManufacture);
        Task<Result<bool>> EditManufacture(Manufacture newManufacture);
        Task<Result<bool>> RemoveManufacture(string manufactureId);
        Task<Result<List<Manufacture>>> GetAllManufacture();
        Task<Result<List<Manufacture>>> GetAllManufactureBy(string manufactureId);
    }
}
