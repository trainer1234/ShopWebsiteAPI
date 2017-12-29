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
        private IManufactureTypeRepository _manufactureTypeRepository;
        private IProductRepository _productRepository;
        private IErrorLogRepository _errorLogRepository;

        public ManufactureService(IManufactureRepository manufactureRepository, IErrorLogRepository errorLogRepository,
            IProductRepository productRepository, IManufactureTypeRepository manufactureTypeRepository)
        {
            _manufactureRepository = manufactureRepository;
            _errorLogRepository = errorLogRepository;
            _productRepository = productRepository;
            _manufactureTypeRepository = manufactureTypeRepository;
        }

        public async Task<Result<bool>> AddManufacture(Manufacture newManufacture)
        {
            var result = new Result<bool>();
            try
            {
                var addResult = await _manufactureRepository.Add(newManufacture);
                if (addResult)
                {
                    result.Succeed = result.Content = true;
                }
                else
                {
                    result.Succeed = result.Content = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(13, "Manufacture Existed");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
            return result;
        }

        public async Task<Result<bool>> EditManufacture(Manufacture newManufacture)
        {
            var result = new Result<bool>();
            try
            {
                var editResult = await _manufactureRepository.Edit(newManufacture);
                if (editResult)
                {
                    result.Succeed = result.Content = true;
                }
                else
                {
                    result.Succeed = result.Content = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(12, "Manufacture not exist");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
            return result;
        }

        public async Task<Result<List<Manufacture>>> GetAllManufacture()
        {
            var result = new Result<List<Manufacture>>();
            try
            {
                var manufactures = await _manufactureRepository.GetAll();
                if (manufactures != null && manufactures.Count > 0)
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
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
            return result;
        }

        public async Task<Result<Manufacture>> GetManufactureBy(string manufactureId)
        {
            var result = new Result<Manufacture>();
            try
            {
                var manufacture = await _manufactureRepository.GetBy(manufactureId);
                if (manufacture != null)
                {
                    result.Content = manufacture;
                    result.Succeed = true;
                }
                else
                {
                    result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(12, "Manufacture not exist");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
            return result;
        }

        public async Task<Result<bool>> RemoveManufacture(string manufactureId)
        {
            var result = new Result<bool>();
            try
            {
                var removeProductByManufactureResult = await _productRepository.RemoveProductBy(manufactureId);
                var removeResult = await _manufactureRepository.Remove(manufactureId);
                if (removeResult)
                {
                    result.Succeed = result.Content = true;
                }
                else
                {
                    result.Succeed = result.Content = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(12, "Manufacture not exist");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
            return result;
        }
    }
}
