using ShopWebsite.DAL.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.DAL.Contracts
{
    public interface IProductSpecificTypeRepository
    {
        Task<bool> Add(ProductSpecificType newProductSpecificType);
        Task<bool> Edit(ProductSpecificType newProductSpecificType);
        Task<bool> Remove(string productSpecificTypeId);
        Task<ProductSpecificType> GetBy(string productSpecificTypeId);
        Task<List<ProductSpecificType>> GetAll();
    }
}
