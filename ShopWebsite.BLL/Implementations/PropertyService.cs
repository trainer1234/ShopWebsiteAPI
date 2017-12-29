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

        public Task<Result<bool>> AddProperty(Property newProperty)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> EditProperty(Property newProperty)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<Property>>> GetAllProperty()
        {
            throw new NotImplementedException();
        }

        public Task<Result<Property>> GetPropertyBy(string propertyId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> RemoveProperty(string propertyId)
        {
            throw new NotImplementedException();
        }
    }
}
