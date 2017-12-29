using ShopWebsite.BLL.Contracts;
using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.DAL.Contracts;
using ShopWebsite.DAL.Models.PropertyModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.BLL.Implementations
{
    public class PropertyService : IPropertyService
    {
        private IPropertyRepository _propertyRepository;
        private IErrorLogRepository _errorLogRepository;

        public PropertyService(IPropertyRepository propertyRepository, IErrorLogRepository errorLogRepository)
        {
            _propertyRepository = propertyRepository;
            _errorLogRepository = errorLogRepository;
        }

        public async Task<Result<bool>> AddProperty(Property newProperty)
        {
            var result = new Result<bool>();
            try
            {
                var addResult = await _propertyRepository.Add(newProperty);
                if (addResult)
                {
                    result.Content = true;
                    result.Succeed = true;
                }
                else
                {
                    result.Content = false;
                    result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(22, "Property existed");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
            return result;
        }

        public async Task<Result<bool>> EditProperty(Property newProperty)
        {
            var result = new Result<bool>();
            try
            {
                var editResult = await _propertyRepository.Edit(newProperty);
                if (editResult)
                {
                    result.Content = true;
                    result.Succeed = true;
                }
                else
                {
                    result.Content = false;
                    result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(21, "Property not exist");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
            return result;
        }

        public async Task<Result<List<Property>>> GetAllProperty()
        {
            var result = new Result<List<Property>>();
            try
            {
                var properties = await _propertyRepository.GetAll();
                if (properties != null && properties.Count > 0)
                {
                    result.Content = properties;
                    result.Succeed = true;
                }
                else
                {
                    result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(20, "No Properties");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
            return result;
        }

        public async Task<Result<Property>> GetPropertyBy(string propertyId)
        {
            var result = new Result<Property>();
            try
            {
                var property = await _propertyRepository.GetBy(propertyId);
                if (property != null)
                {
                    result.Content = property;
                    result.Succeed = true;
                }
                else
                {
                    result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(21, "Property not exist");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
            return result;
        }

        public async Task<Result<bool>> RemoveProperty(string propertyId)
        {
            var result = new Result<bool>();
            try
            {
                var removeResult = await _propertyRepository.Remove(propertyId);
                if (removeResult)
                {
                    result.Content = true;
                    result.Succeed = true;
                }
                else
                {
                    result.Content = false;
                    result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(21, "Property not exist");
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
