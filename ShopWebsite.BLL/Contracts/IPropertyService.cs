using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.DAL.Models.PropertyModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.BLL.Contracts
{
    public interface IPropertyService
    {
        Task<Result<bool>> AddProperty(Property newProperty);
        Task<Result<bool>> EditProperty(Property newProperty);
        Task<Result<bool>> RemoveProperty(string propertyId);
        Task<Result<List<Property>>> GetAllProperty();
        Task<Result<Property>> GetPropertyBy(string propertyId);
    }
}
