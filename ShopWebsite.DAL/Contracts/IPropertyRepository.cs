using ShopWebsite.DAL.Models.PropertyModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.DAL.Contracts
{
    public interface IPropertyRepository
    {
        Task<bool> Add(Property newProperty);
        Task<bool> Edit(Property newProperty);
        Task<bool> Remove(string propertyId);
        Task<Property> GetBy(string propertyId);
        Task<List<Property>> GetAll();
    }
}
