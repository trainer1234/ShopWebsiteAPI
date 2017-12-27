using ShopWebsite.BLL.Contracts;
using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.DAL.Contracts;
using ShopWebsite.DAL.Models.ManufactureModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.BLL.Implementations
{
    public class ManufactureService : IManufactureService
    {
        private IManufactureRepository _manufactureRepository;
        private IErrorLogRepository _errorLogRepository;

        public ManufactureService(IManufactureRepository manufactureRepository, IErrorLogRepository errorLogRepository)
        {
            _manufactureRepository = manufactureRepository;
        }

        public async Task<Result<bool>> AddManufacture(Manufacture newManufacture)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<bool>> EditManufacture(Manufacture newManufacture)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<Manufacture>>> GetAllManufacture()
        {
            var result = new Result<List<Manufacture>>();
            var manufactures = await _manufactureRepository.GetAll();
            if(manufactures != null && manufactures.Count > 0)
            {
                result.Content = manufactures;
                result.Succeed = true;
            }
            else
            {
                result.Succeed = false;
                result.Errors = new Dictionary<int, string>();
                result.Errors.Add(11, "No manufactures");
            }

            return result;
        }

        public async Task<Result<List<Manufacture>>> GetAllManufactureBy(string manufactureId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<bool>> RemoveManufacture(string manufactureId)
        {
            throw new NotImplementedException();
        }
    }
}
