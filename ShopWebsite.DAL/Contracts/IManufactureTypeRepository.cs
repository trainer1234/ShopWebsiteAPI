using ShopWebsite.Common.Models.Enums;
using ShopWebsite.DAL.Models.ManufactureModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.DAL.Contracts
{
    public interface IManufactureTypeRepository
    {
        Task<HashSet<Manufacture>> FilterBy(ProductType productType);
        Task<bool> Add(ManufactureType newManufactureType);
        Task<bool> Remove(string manufactureId);
    }
}
